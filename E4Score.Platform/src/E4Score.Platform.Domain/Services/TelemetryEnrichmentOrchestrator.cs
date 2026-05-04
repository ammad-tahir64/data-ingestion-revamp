using E4Score.Platform.Contracts.CacheEntries;
using E4Score.Platform.Contracts.Events;
using E4Score.Platform.Contracts.Interfaces;
using Microsoft.Extensions.Logging;

namespace E4Score.Platform.Domain.Services;

/// <summary>
/// Orchestrates all enrichment steps for a parsed telemetry message:
///   1. Device cache lookup (IMEI → DeviceId, AssetId, CompanyId)
///   2. Geofence evaluation (in-memory polygons)
///   3. Dwell/excursion time calculation (in-memory state)
///   4. Reverse geocoding (Redis cache → Google API)
///   5. Device runtime state update (write back to Redis)
/// </summary>
public sealed class TelemetryEnrichmentOrchestrator
{
    private readonly IDeviceStateCacheService _deviceCache;
    private readonly IDeviceRuntimeStateCache _runtimeCache;
    private readonly IGeofenceEvaluationService _geofence;
    private readonly IDwellTimeCalculationService _dwellTime;
    private readonly IExcursionTimeCalculationService _excursionTime;
    private readonly IReverseGeocodingService _reverseGeocoding;
    private readonly ILogger<TelemetryEnrichmentOrchestrator> _logger;

    private const double MoveThresholdMeters = 804.67;

    public TelemetryEnrichmentOrchestrator(
        IDeviceStateCacheService deviceCache,
        IDeviceRuntimeStateCache runtimeCache,
        IGeofenceEvaluationService geofence,
        IDwellTimeCalculationService dwellTime,
        IExcursionTimeCalculationService excursionTime,
        IReverseGeocodingService reverseGeocoding,
        ILogger<TelemetryEnrichmentOrchestrator> logger)
    {
        _deviceCache = deviceCache;
        _runtimeCache = runtimeCache;
        _geofence = geofence;
        _dwellTime = dwellTime;
        _excursionTime = excursionTime;
        _reverseGeocoding = reverseGeocoding;
        _logger = logger;
    }

    public async Task<EnrichedTelemetryEvent?> EnrichAsync(RawTelemetryMessage raw, CancellationToken ct)
    {
        // 1. Device lookup from Redis
        var deviceEntry = await _deviceCache.GetDeviceByImeiAsync(raw.Imei, ct);
        if (deviceEntry is null)
        {
            _logger.LogWarning("Device not found in cache for IMEI {Imei} — skipping enrichment", raw.Imei);
            return null;
        }

        // 2. Load device runtime state from Redis
        var runtimeState = await _runtimeCache.GetStateAsync(raw.Imei, ct)
                           ?? new DeviceRuntimeState { Imei = raw.Imei };

        // 3. Determine movement
        var distanceFromPrevious = CalculateDistanceMeters(
            raw.Latitude, raw.Longitude,
            runtimeState.LastLatitude ?? raw.Latitude,
            runtimeState.LastLongitude ?? raw.Longitude);

        var isMove = distanceFromPrevious > MoveThresholdMeters;
        var distanceFromDomicile = CalculateDistanceMeters(
            raw.Latitude, raw.Longitude,
            deviceEntry.DomicileLatitude ?? 0, deviceEntry.DomicileLongitude ?? 0);

        // 4. Geofence evaluation
        var geofences = await _deviceCache.GetGeofencesAsync(deviceEntry.CompanyId ?? 0, ct);
        var matchedGeofence = _geofence.Evaluate(raw.Latitude, raw.Longitude, geofences);

        // 5. Dwell / excursion time
        var dwellResult = _dwellTime.Calculate(isMove, raw.Timestamp, runtimeState);
        var excursionResult = _excursionTime.Calculate(isMove, raw.Timestamp, runtimeState, distanceFromDomicile);

        // 6. Reverse geocode (cache-first, Google fallback)
        var geocode = await _reverseGeocoding.GetAddressAsync(raw.Latitude, raw.Longitude, ct);

        // 7. Date of last move
        var dateOfLastMove = isMove ? raw.Timestamp : runtimeState.DateOfLastMove;

        // 8. First move of day
        var firstMoveOfDay = isMove && (runtimeState.LastMoveTime is null
                                        || runtimeState.LastMoveTime.Value.Date < raw.Timestamp.Date);

        // 9. Update runtime state in Redis
        var updatedState = new DeviceRuntimeState
        {
            Imei = raw.Imei,
            LastLatitude = raw.Latitude,
            LastLongitude = raw.Longitude,
            LastEventTime = raw.Timestamp,
            IsMoving = isMove,
            FirstMoveTime = runtimeState.FirstMoveTime,
            LastMoveTime = isMove ? raw.Timestamp : runtimeState.LastMoveTime,
            CurrentGeofenceId = matchedGeofence?.LocationId,
            DwellStartTime = dwellResult.DwellTimeStart,
            ExcursionStartTime = excursionResult.ExcursionTimeStart,
            LastSpeed = raw.Speed,
            DateOfLastMove = dateOfLastMove,
            MovesInLast30Days = runtimeState.MovesInLast30Days
        };
        await _runtimeCache.SetStateAsync(raw.Imei, updatedState, ct);

        return new EnrichedTelemetryEvent
        {
            Imei = raw.Imei,
            DeviceId = deviceEntry.DeviceId,
            AssetId = deviceEntry.AssetId,
            CompanyId = deviceEntry.CompanyId,
            AssetUuid = deviceEntry.AssetUuid,
            AssetName = deviceEntry.AssetName,
            TrackerType = deviceEntry.TrackerType,
            MessageId = raw.MessageId,
            SourceTimestamp = raw.Timestamp,
            ProcessedAt = DateTime.UtcNow,
            DateOfLastMove = dateOfLastMove,
            Latitude = raw.Latitude,
            Longitude = raw.Longitude,
            Address = matchedGeofence is not null ? matchedGeofence.Address : geocode?.Address,
            City = matchedGeofence is not null ? matchedGeofence.City : geocode?.City,
            State = matchedGeofence is not null ? matchedGeofence.State : geocode?.State,
            Postal = matchedGeofence is not null ? matchedGeofence.Postal : geocode?.Postal,
            Country = geocode?.Country,
            GeofenceAddress = matchedGeofence?.Address,
            GeofenceCity = matchedGeofence?.City,
            GeofenceState = matchedGeofence?.State,
            GeofencePostal = matchedGeofence?.Postal,
            LocationName = matchedGeofence?.Name,
            LocationZone = matchedGeofence?.Zone,
            AssetDomicileName = deviceEntry.DomicileName,
            IsMove = isMove,
            FirstMoveOfDay = firstMoveOfDay,
            Speed = raw.Speed,
            Direction = raw.Direction,
            DistanceFromDomicileInMeters = distanceFromDomicile,
            DistanceFromPreviousEventInMeters = distanceFromPrevious,
            Battery = raw.Battery,
            Temperature = raw.Temperature,
            Fuel = raw.Fuel,
            DwellTimeStart = dwellResult.DwellTimeStart,
            DwellTimeDays = dwellResult.DwellTimeDays,
            ExcursionTimeStart = excursionResult.ExcursionTimeStart,
            ExcursionTimeDays = excursionResult.ExcursionTimeDays,
            MovesInLast3Days = deviceEntry.MovesInLast3Days,
            MovesInLast7Days = deviceEntry.MovesInLast7Days,
            MovesInLast30Days = deviceEntry.MovesInLast30Days,
            MovesInLast60Days = deviceEntry.MovesInLast60Days,
            MovesInLast90Days = deviceEntry.MovesInLast90Days,
            RequiresNotification = isMove
        };
    }

    private static double CalculateDistanceMeters(double lat1, double lng1, double lat2, double lng2)
    {
        const double earthRadiusM = 6_371_000;
        var dLat = ToRadians(lat2 - lat1);
        var dLng = ToRadians(lng2 - lng1);
        var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2)
              + Math.Cos(ToRadians(lat1)) * Math.Cos(ToRadians(lat2))
              * Math.Sin(dLng / 2) * Math.Sin(dLng / 2);
        return earthRadiusM * 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
    }

    private static double ToRadians(double degrees) => degrees * Math.PI / 180.0;
}

namespace E4Score.Platform.Contracts.Events;

/// <summary>
/// Fully enriched telemetry event ready for persistence.
/// Populated after cache lookups, geofence evaluation, dwell/excursion calculation
/// and reverse geocoding.
/// </summary>
public sealed class EnrichedTelemetryEvent
{
    // Identity
    public string Imei { get; init; } = string.Empty;
    public long? DeviceId { get; init; }
    public long? AssetId { get; init; }
    public long? CompanyId { get; init; }
    public string? AssetUuid { get; init; }
    public string? AssetName { get; init; }
    public string? TrackerType { get; init; }
    public string? MessageId { get; init; }

    // Timestamps
    public DateTime SourceTimestamp { get; init; }
    public DateTime ProcessedAt { get; init; } = DateTime.UtcNow;
    public DateTime? DateOfLastMove { get; init; }

    // Location
    public double Latitude { get; init; }
    public double Longitude { get; init; }

    // Reverse geocode
    public string? Address { get; init; }
    public string? City { get; init; }
    public string? State { get; init; }
    public string? Postal { get; init; }
    public string? Country { get; init; }

    // Geofence
    public string? GeofenceAddress { get; init; }
    public string? GeofenceCity { get; init; }
    public string? GeofenceState { get; init; }
    public string? GeofencePostal { get; init; }
    public string? LocationName { get; init; }
    public int? LocationZone { get; init; }
    public string? AssetDomicileName { get; init; }

    // Movement
    public bool IsMove { get; init; }
    public bool FirstMoveOfDay { get; init; }
    public float? Speed { get; init; }
    public string? Direction { get; init; }
    public double? DistanceFromDomicileInMeters { get; init; }
    public double? DistanceFromPreviousEventInMeters { get; init; }

    // Sensors
    public float? Battery { get; init; }
    public float? Temperature { get; init; }
    public int? Fuel { get; init; }

    // Dwell / Excursion
    public DateTime? DwellTimeStart { get; init; }
    public long? DwellTimeDays { get; init; }
    public DateTime? ExcursionTimeStart { get; init; }
    public long? ExcursionTimeDays { get; init; }

    // Move history
    public int? MovesInLast3Days { get; init; }
    public int? MovesInLast7Days { get; init; }
    public int? MovesInLast30Days { get; init; }
    public int? MovesInLast60Days { get; init; }
    public int? MovesInLast90Days { get; init; }

    // Notifications
    public bool RequiresNotification { get; init; }
}

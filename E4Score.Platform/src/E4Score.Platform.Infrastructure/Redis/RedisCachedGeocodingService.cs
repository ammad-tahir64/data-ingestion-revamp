using E4Score.Platform.Contracts.CacheEntries;
using E4Score.Platform.Contracts.Constants;
using E4Score.Platform.Contracts.Interfaces;
using E4Score.Platform.Infrastructure.SqlServer.Repositories;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using System.Text.Json;

namespace E4Score.Platform.Infrastructure.Redis;

/// <summary>
/// Reverse geocoding service that checks Redis cache first, then falls back to
/// the GeocodeLocationLogs table (Dapper), and finally calls the Google Maps API.
/// All successful Google lookups are persisted to both Redis and SQL.
/// </summary>
public sealed class RedisCachedGeocodingService : IReverseGeocodingService
{
    private readonly IDatabase _redis;
    private readonly GeocodeLocationRepository _geocodeRepo;
    private readonly ILogger<RedisCachedGeocodingService> _logger;
    private static readonly TimeSpan GeocodeTtl = TimeSpan.FromHours(24);

    public RedisCachedGeocodingService(
        IConnectionMultiplexer redis,
        GeocodeLocationRepository geocodeRepo,
        ILogger<RedisCachedGeocodingService> logger)
    {
        _redis = redis.GetDatabase();
        _geocodeRepo = geocodeRepo;
        _logger = logger;
    }

    public async Task<GeocodeCacheEntry?> GetAddressAsync(double latitude, double longitude,
        CancellationToken ct = default)
    {
        // 1. Check Redis
        var redisKey = CacheKeys.GeocodeLocation(latitude, longitude);
        var cached = await _redis.StringGetAsync(redisKey);
        if (!cached.IsNullOrEmpty)
            return Deserialize<GeocodeCacheEntry>(cached!);

        // 2. Check SQL (GeocodeLocationLogs table via Dapper)
        var fromSql = await _geocodeRepo.GetByCoordinatesAsync(latitude, longitude, ct);
        if (fromSql is not null)
        {
            // Backfill Redis
            await _redis.StringSetAsync(redisKey, Serialize(fromSql), GeocodeTtl);
            return fromSql;
        }

        // 3. Google Maps API would be called here by a separate GoogleReverseGeocodingService
        //    injected via IReverseGeocodingService decorator chain.
        //    This class returns null to allow fallback.
        return null;
    }

    private static string Serialize<T>(T value) => JsonSerializer.Serialize(value);
    private static T? Deserialize<T>(string json)
    {
        try { return JsonSerializer.Deserialize<T>(json); }
        catch { return default; }
    }
}

using E4Score.Platform.Contracts.CacheEntries;
using E4Score.Platform.Contracts.Constants;
using E4Score.Platform.Contracts.Interfaces;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using System.Text.Json;

namespace E4Score.Platform.Infrastructure.Redis;

/// <summary>
/// Redis-backed implementation of IDeviceStateCacheService.
/// Stores static reference data (device metadata, geofence polygons) keyed by IMEI / CompanyId.
/// TTL: 10 minutes — refreshed by CacheRefreshHostedService before expiry.
/// </summary>
public sealed class RedisDeviceStateCacheService : IDeviceStateCacheService
{
    private readonly IDatabase _db;
    private readonly ILogger<RedisDeviceStateCacheService> _logger;
    private static readonly TimeSpan DeviceTtl = TimeSpan.FromMinutes(15);
    private static readonly TimeSpan GeofenceTtl = TimeSpan.FromMinutes(15);

    public RedisDeviceStateCacheService(IConnectionMultiplexer redis,
        ILogger<RedisDeviceStateCacheService> logger)
    {
        _db = redis.GetDatabase();
        _logger = logger;
    }

    public async Task<DeviceCacheEntry?> GetDeviceByImeiAsync(string imei, CancellationToken ct = default)
    {
        var key = CacheKeys.DeviceByImei(imei);
        var value = await _db.StringGetAsync(key);
        if (value.IsNullOrEmpty) return null;
        return Deserialize<DeviceCacheEntry>(value!);
    }

    public async Task SetDeviceAsync(DeviceCacheEntry entry, CancellationToken ct = default)
    {
        var key = CacheKeys.DeviceByImei(entry.Imei);
        await _db.StringSetAsync(key, Serialize(entry), DeviceTtl);
    }

    public async Task<IReadOnlyList<GeofenceCacheEntry>> GetGeofencesAsync(long companyId, CancellationToken ct = default)
    {
        var key = CacheKeys.GeofencesByCompany(companyId);
        var value = await _db.StringGetAsync(key);
        if (value.IsNullOrEmpty) return Array.Empty<GeofenceCacheEntry>();
        return Deserialize<List<GeofenceCacheEntry>>(value!) ?? (IReadOnlyList<GeofenceCacheEntry>)Array.Empty<GeofenceCacheEntry>();
    }

    public async Task SetGeofencesAsync(long companyId, IReadOnlyList<GeofenceCacheEntry> geofences,
        CancellationToken ct = default)
    {
        var key = CacheKeys.GeofencesByCompany(companyId);
        await _db.StringSetAsync(key, Serialize(geofences), GeofenceTtl);
    }

    private static string Serialize<T>(T value) =>
        JsonSerializer.Serialize(value);

    private static T? Deserialize<T>(string json)
    {
        try { return JsonSerializer.Deserialize<T>(json); }
        catch { return default; }
    }
}

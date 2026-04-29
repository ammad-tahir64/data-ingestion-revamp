using E4Score.Platform.Contracts.CacheEntries;
using E4Score.Platform.Contracts.Constants;
using E4Score.Platform.Contracts.Interfaces;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using System.Text.Json;

namespace E4Score.Platform.Infrastructure.Redis;

/// <summary>
/// Redis-backed implementation of IDeviceRuntimeStateCache.
/// Stores transient device state (last position, move timestamps, dwell/excursion state)
/// keyed by IMEI. Updated on every telemetry message — replaces per-message SQL Server writes.
/// TTL: 24 hours (rebuilt from PingEvents on cache miss or restart).
/// </summary>
public sealed class RedisDeviceRuntimeStateCache : IDeviceRuntimeStateCache
{
    private readonly IDatabase _db;
    private readonly ILogger<RedisDeviceRuntimeStateCache> _logger;
    private static readonly TimeSpan StateTtl = TimeSpan.FromHours(24);

    public RedisDeviceRuntimeStateCache(IConnectionMultiplexer redis,
        ILogger<RedisDeviceRuntimeStateCache> logger)
    {
        _db = redis.GetDatabase();
        _logger = logger;
    }

    public async Task<DeviceRuntimeState?> GetStateAsync(string imei, CancellationToken ct = default)
    {
        var key = CacheKeys.DeviceState(imei);
        var value = await _db.StringGetAsync(key);
        if (value.IsNullOrEmpty) return null;
        return Deserialize<DeviceRuntimeState>(value!);
    }

    public async Task SetStateAsync(string imei, DeviceRuntimeState state, CancellationToken ct = default)
    {
        var key = CacheKeys.DeviceState(imei);
        await _db.StringSetAsync(key, Serialize(state), StateTtl);
    }

    private static string Serialize<T>(T value) => JsonSerializer.Serialize(value);

    private static T? Deserialize<T>(string json)
    {
        try { return JsonSerializer.Deserialize<T>(json); }
        catch { return default; }
    }
}

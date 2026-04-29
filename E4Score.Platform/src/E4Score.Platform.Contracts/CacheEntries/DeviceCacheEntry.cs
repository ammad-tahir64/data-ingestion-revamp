namespace E4Score.Platform.Contracts.CacheEntries;

/// <summary>
/// Static device/asset reference data cached in Redis.
/// Loaded at startup by CacheWarmupHostedService and refreshed every 5 minutes.
/// Key: device:imei:{imei}
/// </summary>
public sealed class DeviceCacheEntry
{
    public string Imei { get; init; } = string.Empty;
    public long DeviceId { get; init; }
    public long? AssetId { get; init; }
    public long? CompanyId { get; init; }
    public string? AssetUuid { get; init; }
    public string? AssetName { get; init; }
    public string? TrackerType { get; init; }
    public double? DomicileLatitude { get; init; }
    public double? DomicileLongitude { get; init; }
    public string? DomicileName { get; init; }
    public int? MovesInLast3Days { get; init; }
    public int? MovesInLast7Days { get; init; }
    public int? MovesInLast30Days { get; init; }
    public int? MovesInLast60Days { get; init; }
    public int? MovesInLast90Days { get; init; }
}

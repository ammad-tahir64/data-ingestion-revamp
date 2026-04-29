using E4Score.Platform.Contracts.CacheEntries;

namespace E4Score.Platform.Contracts.Interfaces;

/// <summary>
/// Read-only reference-data cache (device metadata, geofences, company settings).
/// Loaded at startup and refreshed every 5 minutes. Does not change per message.
/// </summary>
public interface IDeviceStateCacheService
{
    Task<DeviceCacheEntry?> GetDeviceByImeiAsync(string imei, CancellationToken ct = default);
    Task SetDeviceAsync(DeviceCacheEntry entry, CancellationToken ct = default);
    Task<IReadOnlyList<GeofenceCacheEntry>> GetGeofencesAsync(long companyId, CancellationToken ct = default);
    Task SetGeofencesAsync(long companyId, IReadOnlyList<GeofenceCacheEntry> geofences, CancellationToken ct = default);
}

using E4Score.Platform.Contracts.CacheEntries;

namespace E4Score.Platform.Contracts.Interfaces;

/// <summary>
/// Write-heavy device runtime state cache.
/// Updated on every telemetry message to avoid per-message SQL Server roundtrips.
/// </summary>
public interface IDeviceRuntimeStateCache
{
    Task<DeviceRuntimeState?> GetStateAsync(string imei, CancellationToken ct = default);
    Task SetStateAsync(string imei, DeviceRuntimeState state, CancellationToken ct = default);
}

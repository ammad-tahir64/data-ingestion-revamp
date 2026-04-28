using E4Score.Platform.Infrastructure.SqlServer.Repositories;
using E4Score.Platform.Contracts.Interfaces;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace E4Score.Platform.Worker.Workers;

/// <summary>
/// Runs continuously in the background, refreshing Redis device and geofence caches
/// every 5 minutes so stale data never causes a cache miss on the hot path.
/// </summary>
public sealed class CacheRefreshHostedService : BackgroundService
{
    private readonly DeviceRepository _deviceRepo;
    private readonly GeofenceRepository _geofenceRepo;
    private readonly IDeviceStateCacheService _deviceCache;
    private readonly ILogger<CacheRefreshHostedService> _logger;
    private readonly TimeSpan _interval = TimeSpan.FromMinutes(5);

    public CacheRefreshHostedService(
        DeviceRepository deviceRepo,
        GeofenceRepository geofenceRepo,
        IDeviceStateCacheService deviceCache,
        ILogger<CacheRefreshHostedService> logger)
    {
        _deviceRepo = deviceRepo;
        _geofenceRepo = geofenceRepo;
        _deviceCache = deviceCache;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken ct)
    {
        // Wait for initial warm-up to complete before starting refresh cycle
        while (!CacheWarmupHostedService.IsWarmedUp && !ct.IsCancellationRequested)
            await Task.Delay(TimeSpan.FromSeconds(1), ct);

        _logger.LogInformation("CacheRefreshHostedService started — refresh interval: {Interval}", _interval);

        using var timer = new PeriodicTimer(_interval);
        while (await timer.WaitForNextTickAsync(ct))
        {
            try
            {
                _logger.LogDebug("Starting periodic cache refresh");

                var devices = await _deviceRepo.GetAllActiveDevicesAsync(ct);
                await Task.WhenAll(devices.Select(d => _deviceCache.SetDeviceAsync(d, ct)));

                var geofencesByCompany = await _geofenceRepo.GetAllGeofencesGroupedByCompanyAsync(ct);
                await Task.WhenAll(geofencesByCompany.Select(kvp =>
                    _deviceCache.SetGeofencesAsync(kvp.Key, kvp.Value, ct)));

                _logger.LogDebug("Cache refresh complete — {DeviceCount} devices, {CompanyCount} companies",
                    devices.Count, geofencesByCompany.Count);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Cache refresh failed — will retry in {Interval}", _interval);
            }
        }
    }
}

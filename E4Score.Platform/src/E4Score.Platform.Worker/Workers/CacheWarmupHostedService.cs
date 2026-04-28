using E4Score.Platform.Contracts.CacheEntries;
using E4Score.Platform.Contracts.Interfaces;
using E4Score.Platform.Infrastructure.SqlServer.Repositories;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace E4Score.Platform.Worker.Workers;

/// <summary>
/// Runs once at startup. Loads all active device metadata and geofence polygons
/// from SQL Server (Dapper) into Redis, so the hot message path never touches SQL.
///
/// Readiness probe blocks until this service completes successfully.
/// For 50,000 devices: ~5 seconds. For 500,000: ~30 seconds.
/// </summary>
public sealed class CacheWarmupHostedService : BackgroundService
{
    private readonly DeviceRepository _deviceRepo;
    private readonly GeofenceRepository _geofenceRepo;
    private readonly IDeviceStateCacheService _deviceCache;
    private readonly ILogger<CacheWarmupHostedService> _logger;

    public static bool IsWarmedUp { get; private set; }

    public CacheWarmupHostedService(
        DeviceRepository deviceRepo,
        GeofenceRepository geofenceRepo,
        IDeviceStateCacheService deviceCache,
        ILogger<CacheWarmupHostedService> logger)
    {
        _deviceRepo = deviceRepo;
        _geofenceRepo = geofenceRepo;
        _deviceCache = deviceCache;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken ct)
    {
        _logger.LogInformation("CacheWarmupHostedService starting cache warm-up...");
        var sw = System.Diagnostics.Stopwatch.StartNew();

        try
        {
            // 1. Warm device reference cache
            var devices = await _deviceRepo.GetAllActiveDevicesAsync(ct);
            var deviceTasks = devices.Select(d => _deviceCache.SetDeviceAsync(d, ct));
            await Task.WhenAll(deviceTasks);
            _logger.LogInformation("Cached {Count} devices in {Elapsed}ms", devices.Count, sw.ElapsedMilliseconds);

            // 2. Warm geofence cache per company
            var geofencesByCompany = await _geofenceRepo.GetAllGeofencesGroupedByCompanyAsync(ct);
            var geofenceTasks = geofencesByCompany.Select(kvp =>
                _deviceCache.SetGeofencesAsync(kvp.Key, kvp.Value, ct));
            await Task.WhenAll(geofenceTasks);
            _logger.LogInformation("Cached geofences for {Count} companies in {Elapsed}ms",
                geofencesByCompany.Count, sw.ElapsedMilliseconds);

            IsWarmedUp = true;
            _logger.LogInformation("Cache warm-up complete in {Elapsed}ms", sw.ElapsedMilliseconds);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Cache warm-up failed after {Elapsed}ms", sw.ElapsedMilliseconds);
            // Do not set IsWarmedUp — readiness probe will block
            throw;
        }
    }
}

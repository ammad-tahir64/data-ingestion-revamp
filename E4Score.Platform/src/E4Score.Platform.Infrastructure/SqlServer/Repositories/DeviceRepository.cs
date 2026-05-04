using Dapper;
using E4Score.Platform.Contracts.CacheEntries;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace E4Score.Platform.Infrastructure.SqlServer.Repositories;

/// <summary>
/// Reads device and asset reference data from SQL Server using Dapper.
/// Called only at startup (CacheWarmupHostedService) and every 5 minutes
/// (CacheRefreshHostedService) — never on the hot message path.
/// </summary>
public sealed class DeviceRepository
{
    private readonly string _connectionString;
    private readonly ILogger<DeviceRepository> _logger;

    public DeviceRepository(string connectionString, ILogger<DeviceRepository> logger)
    {
        _connectionString = connectionString;
        _logger = logger;
    }

    /// <summary>
    /// Returns all active devices joined with their asset and domicile location.
    /// Used to warm/refresh the Redis device cache.
    /// </summary>
    public async Task<IReadOnlyList<DeviceCacheEntry>> GetAllActiveDevicesAsync(CancellationToken ct = default)
    {
        const string sql = """
            SELECT
                d.imei                      AS Imei,
                d.id                        AS DeviceId,
                d.asset_id                  AS AssetId,
                d.owner_id                  AS CompanyId,
                a.uuid                      AS AssetUuid,
                a.asset_id                  AS AssetName,
                d.tracker_type              AS TrackerType,
                l.latitude                  AS DomicileLatitude,
                l.longitude                 AS DomicileLongitude,
                l.name                      AS DomicileName,
                d.moves_in_last3days        AS MovesInLast3Days,
                d.moves_in_last7days        AS MovesInLast7Days,
                d.moves_in_last30days       AS MovesInLast30Days,
                d.moves_in_last60days       AS MovesInLast60Days,
                d.moves_in_last90days       AS MovesInLast90Days
            FROM eztrack_device d
            INNER JOIN asset a ON d.asset_id = a.id
            LEFT  JOIN location l ON a.domicile_id = l.id
            WHERE d.deleted = 0
              AND d.asset_id IS NOT NULL
            """;

        await using var connection = new SqlConnection(_connectionString);
        var result = await connection.QueryAsync<DeviceCacheEntry>(
            new CommandDefinition(sql, cancellationToken: ct));

        var list = result.AsList();
        _logger.LogInformation("Loaded {Count} active devices from SQL Server", list.Count);
        return list;
    }

    /// <summary>
    /// Returns a single device cache entry by IMEI.
    /// Used as a cache fallback when a device is not found in Redis.
    /// </summary>
    public async Task<DeviceCacheEntry?> GetDeviceByImeiAsync(string imei, CancellationToken ct = default)
    {
        const string sql = """
            SELECT
                d.imei                      AS Imei,
                d.id                        AS DeviceId,
                d.asset_id                  AS AssetId,
                d.owner_id                  AS CompanyId,
                a.uuid                      AS AssetUuid,
                a.asset_id                  AS AssetName,
                d.tracker_type              AS TrackerType,
                l.latitude                  AS DomicileLatitude,
                l.longitude                 AS DomicileLongitude,
                l.name                      AS DomicileName,
                d.moves_in_last3days        AS MovesInLast3Days,
                d.moves_in_last7days        AS MovesInLast7Days,
                d.moves_in_last30days       AS MovesInLast30Days,
                d.moves_in_last60days       AS MovesInLast60Days,
                d.moves_in_last90days       AS MovesInLast90Days
            FROM eztrack_device d
            INNER JOIN asset a ON d.asset_id = a.id
            LEFT  JOIN location l ON a.domicile_id = l.id
            WHERE d.imei = @Imei
              AND d.deleted = 0
              AND d.asset_id IS NOT NULL
            """;

        await using var connection = new SqlConnection(_connectionString);
        return await connection.QueryFirstOrDefaultAsync<DeviceCacheEntry>(
            new CommandDefinition(sql, new { Imei = imei }, cancellationToken: ct));
    }
}

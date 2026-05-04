using Dapper;
using E4Score.Platform.Contracts.CacheEntries;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace E4Score.Platform.Infrastructure.SqlServer.Repositories;

/// <summary>
/// Reads geofence / location data from SQL Server using Dapper.
/// Called only at startup and during cache refresh — never on the hot message path.
/// </summary>
public sealed class GeofenceRepository
{
    private readonly string _connectionString;
    private readonly ILogger<GeofenceRepository> _logger;

    public GeofenceRepository(string connectionString, ILogger<GeofenceRepository> logger)
    {
        _connectionString = connectionString;
        _logger = logger;
    }

    /// <summary>
    /// Returns all geofence locations grouped by company for cache warming.
    /// </summary>
    public async Task<IReadOnlyDictionary<long, IReadOnlyList<GeofenceCacheEntry>>> GetAllGeofencesGroupedByCompanyAsync(
        CancellationToken ct = default)
    {
        const string sql = """
            SELECT
                l.id                    AS LocationId,
                l.name                  AS Name,
                l.street_address        AS Address,
                l.locality              AS City,
                l.state                 AS State,
                l.postal                AS Postal,
                COALESCE(l.zone, 0)     AS Zone,
                l.latitude              AS CenterLatitude,
                l.longitude             AS CenterLongitude,
                COALESCE(l.radius, 402.336) AS RadiusMeters,
                l.polygon_wkt           AS PolygonWkt,
                l.company_id            AS CompanyId
            FROM location l
            WHERE l.deleted = 0
              AND l.latitude IS NOT NULL
              AND l.longitude IS NOT NULL
            """;

        await using var connection = new SqlConnection(_connectionString);
        var rows = await connection.QueryAsync<GeofenceRow>(
            new CommandDefinition(sql, cancellationToken: ct));

        var grouped = rows
            .GroupBy(r => r.CompanyId)
            .ToDictionary(
                g => g.Key,
                g => (IReadOnlyList<GeofenceCacheEntry>)g.Select(r => new GeofenceCacheEntry
                {
                    LocationId = r.LocationId,
                    Name = r.Name,
                    Address = r.Address,
                    City = r.City,
                    State = r.State,
                    Postal = r.Postal,
                    Zone = r.Zone,
                    CenterLatitude = r.CenterLatitude,
                    CenterLongitude = r.CenterLongitude,
                    RadiusMeters = r.RadiusMeters,
                    PolygonWkt = r.PolygonWkt
                }).ToList());

        _logger.LogInformation("Loaded geofences for {CompanyCount} companies from SQL Server", grouped.Count);
        return grouped;
    }

    private sealed class GeofenceRow
    {
        public long LocationId { get; init; }
        public string Name { get; init; } = string.Empty;
        public string? Address { get; init; }
        public string? City { get; init; }
        public string? State { get; init; }
        public string? Postal { get; init; }
        public int Zone { get; init; }
        public double CenterLatitude { get; init; }
        public double CenterLongitude { get; init; }
        public double RadiusMeters { get; init; }
        public string? PolygonWkt { get; init; }
        public long CompanyId { get; init; }
    }
}

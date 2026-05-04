using Dapper;
using E4Score.Platform.Contracts.CacheEntries;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace E4Score.Platform.Infrastructure.SqlServer.Repositories;

/// <summary>
/// Reads and writes reverse-geocode results to the GeocodeLocationLogs table using Dapper.
/// </summary>
public sealed class GeocodeLocationRepository
{
    private readonly string _connectionString;
    private readonly ILogger<GeocodeLocationRepository> _logger;

    public GeocodeLocationRepository(string connectionString, ILogger<GeocodeLocationRepository> logger)
    {
        _connectionString = connectionString;
        _logger = logger;
    }

    public async Task<GeocodeCacheEntry?> GetByCoordinatesAsync(
        double latitude, double longitude, CancellationToken ct = default)
    {
        // Match on 3 decimal places (≈111m accuracy) — consistent with existing logic
        var latStr = latitude.ToString("F3");
        var lngStr = longitude.ToString("F3");

        const string sql = """
            SELECT TOP 1
                street_address  AS Address,
                locality        AS City,
                state           AS State,
                postal          AS Postal,
                country         AS Country
            FROM geocode_location_logs
            WHERE latitude  = @Lat
              AND longitude = @Lng
            """;

        await using var connection = new SqlConnection(_connectionString);
        return await connection.QueryFirstOrDefaultAsync<GeocodeCacheEntry>(
            new CommandDefinition(sql, new { Lat = latStr, Lng = lngStr }, cancellationToken: ct));
    }

    public async Task SaveAsync(double latitude, double longitude, GeocodeCacheEntry entry,
        CancellationToken ct = default)
    {
        var latStr = latitude.ToString("F3");
        var lngStr = longitude.ToString("F3");

        const string sql = """
            IF NOT EXISTS (
                SELECT 1 FROM geocode_location_logs
                WHERE latitude = @Lat AND longitude = @Lng
            )
            INSERT INTO geocode_location_logs
                (latitude, longitude, street_address, locality, state, postal, country, created, version)
            VALUES
                (@Lat, @Lng, @Address, @City, @State, @Postal, @Country, GETUTCDATE(), 0)
            """;

        await using var connection = new SqlConnection(_connectionString);
        await connection.ExecuteAsync(
            new CommandDefinition(sql,
                new
                {
                    Lat = latStr, Lng = lngStr,
                    entry.Address, City = entry.City, entry.State, entry.Postal, entry.Country
                },
                cancellationToken: ct));
    }
}

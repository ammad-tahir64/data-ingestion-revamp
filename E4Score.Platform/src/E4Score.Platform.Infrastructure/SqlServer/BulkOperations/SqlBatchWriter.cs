using Dapper;
using E4Score.Platform.Contracts.Events;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using System.Data;

namespace E4Score.Platform.Infrastructure.SqlServer.BulkOperations;

/// <summary>
/// Performs micro-batched persistence of enriched telemetry events using:
///   - SqlBulkCopy for append-only insert tables (PingSensors, PingLocations, EztrackEvents)
///   - Dapper MERGE (upsert) for state tables (EztrackDevice, Asset)
///
/// Called every 1–5 seconds or when the batch reaches 500 events — whichever comes first.
/// A single call converts 500 individual EF Core INSERTs into 3 SqlBulkCopy calls + 2 MERGE statements.
/// </summary>
public sealed class SqlBatchWriter
{
    private readonly string _connectionString;
    private readonly ILogger<SqlBatchWriter> _logger;

    public SqlBatchWriter(string connectionString, ILogger<SqlBatchWriter> logger)
    {
        _connectionString = connectionString;
        _logger = logger;
    }

    public async Task FlushAsync(IReadOnlyList<EnrichedTelemetryEvent> batch, CancellationToken ct)
    {
        if (batch.Count == 0) return;

        await using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync(ct);
        await using var transaction = await connection.BeginTransactionAsync(ct) as SqlTransaction;

        try
        {
            // Step 1: Bulk insert PingSensors
            var sensorTable = BuildPingSensorTable(batch);
            await BulkInsertAsync(connection, transaction!, "ping_sensor", sensorTable, ct);

            // Step 2: Bulk insert PingLocations
            var locationTable = BuildPingLocationTable(batch);
            await BulkInsertAsync(connection, transaction!, "ping_location", locationTable, ct);

            // Step 3: Bulk insert EztrackEvents
            var eventTable = BuildEztrackEventTable(batch);
            await BulkInsertAsync(connection, transaction!, "eztrack_event", eventTable, ct);

            // Step 4: Upsert device state (last position, dwell, excursion, move dates)
            await UpsertDeviceStatesAsync(connection, transaction!, batch, ct);

            // Step 5: Upsert asset state
            await UpsertAssetStatesAsync(connection, transaction!, batch, ct);

            await transaction!.CommitAsync(ct);
            _logger.LogDebug("Flushed batch of {Count} events to SQL Server", batch.Count);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Batch flush failed for {Count} events — rolling back", batch.Count);
            await transaction!.RollbackAsync(ct);
            throw;
        }
    }

    // ── SqlBulkCopy helpers ──────────────────────────────────────────────────

    private static async Task BulkInsertAsync(SqlConnection connection, SqlTransaction transaction,
        string tableName, DataTable table, CancellationToken ct)
    {
        using var bulk = new SqlBulkCopy(connection, SqlBulkCopyOptions.Default, transaction)
        {
            DestinationTableName = tableName,
            BatchSize = 1000,
            BulkCopyTimeout = 60
        };

        foreach (DataColumn col in table.Columns)
            bulk.ColumnMappings.Add(col.ColumnName, col.ColumnName);

        await bulk.WriteToServerAsync(table, ct);
    }

    // ── DataTable builders ───────────────────────────────────────────────────

    private static DataTable BuildPingSensorTable(IReadOnlyList<EnrichedTelemetryEvent> batch)
    {
        var table = new DataTable("ping_sensor");
        table.Columns.Add("created", typeof(DateTime));
        table.Columns.Add("deleted", typeof(int));
        table.Columns.Add("enabled", typeof(int));
        table.Columns.Add("uuid", typeof(string));
        table.Columns.Add("version", typeof(int));
        table.Columns.Add("battery", typeof(float));
        table.Columns.Add("temperature", typeof(float));

        foreach (var e in batch)
        {
            var row = table.NewRow();
            row["created"] = e.ProcessedAt;
            row["deleted"] = 0;
            row["enabled"] = 1;
            row["uuid"] = e.MessageId ?? Guid.NewGuid().ToString();
            row["version"] = 1;
            row["battery"] = (object?)e.Battery ?? DBNull.Value;
            row["temperature"] = (object?)e.Temperature ?? DBNull.Value;
            table.Rows.Add(row);
        }
        return table;
    }

    private static DataTable BuildPingLocationTable(IReadOnlyList<EnrichedTelemetryEvent> batch)
    {
        var table = new DataTable("ping_location");
        table.Columns.Add("created", typeof(DateTime));
        table.Columns.Add("deleted", typeof(int));
        table.Columns.Add("enabled", typeof(int));
        table.Columns.Add("uuid", typeof(string));
        table.Columns.Add("version", typeof(int));
        table.Columns.Add("latitude", typeof(double));
        table.Columns.Add("longitude", typeof(double));

        foreach (var e in batch)
        {
            var row = table.NewRow();
            row["created"] = e.ProcessedAt;
            row["deleted"] = 0;
            row["enabled"] = 1;
            row["uuid"] = Guid.NewGuid().ToString();
            row["version"] = 1;
            row["latitude"] = e.Latitude;
            row["longitude"] = e.Longitude;
            table.Rows.Add(row);
        }
        return table;
    }

    private static DataTable BuildEztrackEventTable(IReadOnlyList<EnrichedTelemetryEvent> batch)
    {
        var table = new DataTable("eztrack_event");
        table.Columns.Add("created", typeof(DateTime));
        table.Columns.Add("deleted", typeof(int));
        table.Columns.Add("enabled", typeof(int));
        table.Columns.Add("uuid", typeof(string));
        table.Columns.Add("version", typeof(int));
        table.Columns.Add("updated", typeof(DateTime));
        table.Columns.Add("imei", typeof(string));
        table.Columns.Add("asset_uuid", typeof(string));
        table.Columns.Add("asset_name", typeof(string));
        table.Columns.Add("asset_domicile_name", typeof(string));
        table.Columns.Add("tracker_type", typeof(string));
        table.Columns.Add("source_timestamp", typeof(DateTime));
        table.Columns.Add("date_of_last_move", typeof(DateTime));
        table.Columns.Add("is_move", typeof(int));
        table.Columns.Add("first_move_of_day", typeof(int));
        table.Columns.Add("speed", typeof(float));
        table.Columns.Add("direction", typeof(string));
        table.Columns.Add("address", typeof(string));
        table.Columns.Add("city", typeof(string));
        table.Columns.Add("state", typeof(string));
        table.Columns.Add("postal", typeof(string));
        table.Columns.Add("geofence_address", typeof(string));
        table.Columns.Add("geofence_city", typeof(string));
        table.Columns.Add("geofence_state", typeof(string));
        table.Columns.Add("geofence_postal", typeof(string));
        table.Columns.Add("location_name", typeof(string));
        table.Columns.Add("zone", typeof(int));
        table.Columns.Add("distance_from_domicile_in_meters", typeof(double));
        table.Columns.Add("distance_from_previous_event_in_meters", typeof(double));
        table.Columns.Add("battery", typeof(float));
        table.Columns.Add("fuel", typeof(int));
        table.Columns.Add("dwell_time_start", typeof(DateTime));
        table.Columns.Add("dwell_time", typeof(long));
        // NOTE: "excrusion" is intentionally misspelled to match the existing DB column names
        // (excrusion_time_start, excrusion_time). Changing the DB column names is out of scope.
        table.Columns.Add("excrusion_time_start", typeof(DateTime));
        table.Columns.Add("excrusion_time", typeof(long));
        table.Columns.Add("moves_in_last30days", typeof(int));
        table.Columns.Add("move_threshold_in_meters", typeof(double));

        foreach (var e in batch)
        {
            var row = table.NewRow();
            row["created"] = e.ProcessedAt;
            row["deleted"] = 0;
            row["enabled"] = 1;
            row["uuid"] = Guid.NewGuid().ToString();
            row["version"] = 1;
            row["updated"] = e.ProcessedAt;
            row["imei"] = e.Imei;
            row["asset_uuid"] = (object?)e.AssetUuid ?? DBNull.Value;
            row["asset_name"] = (object?)e.AssetName ?? DBNull.Value;
            row["asset_domicile_name"] = (object?)e.AssetDomicileName ?? DBNull.Value;
            row["tracker_type"] = (object?)e.TrackerType ?? DBNull.Value;
            row["source_timestamp"] = e.SourceTimestamp;
            row["date_of_last_move"] = (object?)e.DateOfLastMove ?? DBNull.Value;
            row["is_move"] = e.IsMove ? 1 : 0;
            row["first_move_of_day"] = e.FirstMoveOfDay ? 1 : 0;
            row["speed"] = (object?)e.Speed ?? DBNull.Value;
            row["direction"] = (object?)e.Direction ?? DBNull.Value;
            row["address"] = (object?)e.Address ?? DBNull.Value;
            row["city"] = (object?)e.City ?? DBNull.Value;
            row["state"] = (object?)e.State ?? DBNull.Value;
            row["postal"] = (object?)e.Postal ?? DBNull.Value;
            row["geofence_address"] = (object?)e.GeofenceAddress ?? DBNull.Value;
            row["geofence_city"] = (object?)e.GeofenceCity ?? DBNull.Value;
            row["geofence_state"] = (object?)e.GeofenceState ?? DBNull.Value;
            row["geofence_postal"] = (object?)e.GeofencePostal ?? DBNull.Value;
            row["location_name"] = (object?)e.LocationName ?? DBNull.Value;
            row["zone"] = (object?)e.LocationZone ?? DBNull.Value;
            row["distance_from_domicile_in_meters"] = (object?)e.DistanceFromDomicileInMeters ?? DBNull.Value;
            row["distance_from_previous_event_in_meters"] = (object?)e.DistanceFromPreviousEventInMeters ?? DBNull.Value;
            row["battery"] = (object?)e.Battery ?? DBNull.Value;
            row["fuel"] = (object?)e.Fuel ?? DBNull.Value;
            row["dwell_time_start"] = (object?)e.DwellTimeStart ?? DBNull.Value;
            row["dwell_time"] = (object?)e.DwellTimeDays ?? DBNull.Value;
            row["excrusion_time_start"] = (object?)e.ExcursionTimeStart ?? DBNull.Value;
            row["excrusion_time"] = (object?)e.ExcursionTimeDays ?? DBNull.Value;
            row["moves_in_last30days"] = (object?)e.MovesInLast30Days ?? DBNull.Value;
            row["move_threshold_in_meters"] = 804.67;
            table.Rows.Add(row);
        }
        return table;
    }

    // ── Dapper MERGE for state tables ────────────────────────────────────────

    private static async Task UpsertDeviceStatesAsync(SqlConnection connection, SqlTransaction transaction,
        IReadOnlyList<EnrichedTelemetryEvent> batch, CancellationToken ct)
    {
        // Deduplicate: keep the latest event per IMEI
        var latest = batch
            .GroupBy(e => e.Imei)
            .Select(g => g.OrderByDescending(e => e.SourceTimestamp).First())
            .ToList();

        const string sql = """
            UPDATE eztrack_device SET
                updated                         = @ProcessedAt,
                version                         = version + 1,
                tracker_type                    = @TrackerType,
                battery                         = @Battery,
                last_event_latitude             = @Latitude,
                last_event_longitude            = @Longitude,
                distance_from_domicile_in_meters = @DistanceFromDomicileInMeters,
                asset_id                        = @AssetId,
                latest_event_address            = COALESCE(@GeofenceAddress, @Address),
                latest_event_city               = COALESCE(@GeofenceCity, @City),
                latest_event_state              = COALESCE(@GeofenceState, @State),
                latest_event_postal             = COALESCE(@GeofencePostal, @Postal),
                latest_event_date               = @SourceTimestamp,
                location_name                   = @LocationName,
                dwell_time_start                = @DwellTimeStart,
                excrusion_time_start            = @ExcursionTimeStart,
                date_of_last_move               = @DateOfLastMove
            WHERE imei = @Imei
              AND deleted = 0
            """;

        var parameters = latest.Select(e => new
        {
            e.Imei,
            e.ProcessedAt,
            e.TrackerType,
            e.Battery,
            e.Latitude,
            e.Longitude,
            e.DistanceFromDomicileInMeters,
            e.AssetId,
            e.GeofenceAddress,
            e.Address,
            e.GeofenceCity,
            e.City,
            e.GeofenceState,
            e.State,
            e.GeofencePostal,
            e.Postal,
            e.SourceTimestamp,
            e.LocationName,
            e.DwellTimeStart,
            e.ExcursionTimeStart,
            e.DateOfLastMove
        });

        await connection.ExecuteAsync(
            new CommandDefinition(sql, parameters, transaction: transaction, cancellationToken: ct));
    }

    private static async Task UpsertAssetStatesAsync(SqlConnection connection, SqlTransaction transaction,
        IReadOnlyList<EnrichedTelemetryEvent> batch, CancellationToken ct)
    {
        var latest = batch
            .Where(e => e.AssetUuid is not null)
            .GroupBy(e => e.AssetUuid!)
            .Select(g => g.OrderByDescending(e => e.SourceTimestamp).First())
            .ToList();

        if (latest.Count == 0) return;

        const string sql = """
            UPDATE asset SET
                updated                         = @ProcessedAt,
                version                         = version + 1,
                battery                         = @Battery,
                last_event_latitude             = @Latitude,
                last_event_longitude            = @Longitude,
                distance_from_domicile_in_meters = @DistanceFromDomicileInMeters,
                latest_event_address            = COALESCE(@GeofenceAddress, @Address),
                latest_event_city               = COALESCE(@GeofenceCity, @City),
                latest_event_state              = COALESCE(@GeofenceState, @State),
                latest_event_postal             = COALESCE(@GeofencePostal, @Postal),
                latest_event_date               = @SourceTimestamp,
                location_name                   = @LocationName,
                date_of_last_move               = @DateOfLastMove,
                moves_in_last3days              = @MovesInLast3Days,
                moves_in_last7days              = @MovesInLast7Days,
                moves_in_last30days             = @MovesInLast30Days,
                moves_in_last60days             = @MovesInLast60Days,
                moves_in_last90days             = @MovesInLast90Days,
                temperature_inc                 = @Temperature
            WHERE uuid = @AssetUuid
              AND deleted = 0
            """;

        var parameters = latest.Select(e => new
        {
            e.AssetUuid,
            e.ProcessedAt,
            e.Battery,
            e.Latitude,
            e.Longitude,
            e.DistanceFromDomicileInMeters,
            e.GeofenceAddress,
            e.Address,
            e.GeofenceCity,
            e.City,
            e.GeofenceState,
            e.State,
            e.GeofencePostal,
            e.Postal,
            e.SourceTimestamp,
            e.LocationName,
            e.DateOfLastMove,
            e.MovesInLast3Days,
            e.MovesInLast7Days,
            e.MovesInLast30Days,
            e.MovesInLast60Days,
            e.MovesInLast90Days,
            e.Temperature
        });

        await connection.ExecuteAsync(
            new CommandDefinition(sql, parameters, transaction: transaction, cancellationToken: ct));
    }
}

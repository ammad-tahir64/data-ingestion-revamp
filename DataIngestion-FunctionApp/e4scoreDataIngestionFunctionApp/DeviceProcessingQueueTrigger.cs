using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using e4scoreDataIngestionFunctionApp.Models.Enum;
using e4scoreDataIngestionFunctionApp.Models.RequestModels;
using Newtonsoft.Json;
using System.Collections.Generic;
using e4scoreDataIngestionFunctionApp.Models.DomainModels;
using e4scoreDataIngestionFunctionApp.Models;
using e4scoreDataIngestionFunctionApp.Services;
using Dapper;
using Microsoft.Data.SqlClient;
using SysTask = System.Threading.Tasks.Task;

namespace e4scoreDataIngestionFunctionApp
{
    public class DeviceProcessingQueueTrigger
    {
        private readonly string _connectionString;
        private readonly IE4EAIQueue _e4EAIQueue;
        private readonly ILogger<DeviceProcessingQueueTrigger> _logger;

        public DeviceProcessingQueueTrigger(
            IE4EAIQueue e4EAIQueue,
            ILogger<DeviceProcessingQueueTrigger> logger)
        {
            _e4EAIQueue = e4EAIQueue;
            _logger = logger;
            _connectionString = Environment.GetEnvironmentVariable("SqlConnection")
                ?? throw new InvalidOperationException("SqlConnection environment variable is not set.");
        }

        [Function("DeviceProcessingQueueTrigger")]
        public async SysTask Run(
            [ServiceBusTrigger(ApplicationSettings.DeviceProcessingQueueName, Connection = ApplicationSettings.MaTrackQueueConnection)]
            string myQueueItem,
            CancellationToken ct)
        {
            var deviceProcessing = JsonConvert.DeserializeObject<DeviceProcessing>(myQueueItem)!;
            deviceProcessing.MessageId = Guid.NewGuid().ToString();

            await using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync(ct);
            await using var transaction = await connection.BeginTransactionAsync(ct);

            var watch = System.Diagnostics.Stopwatch.StartNew();
            try
            {
                _logger.LogWarning(
                    "Device Processing Function App Started IMEI: {Imei}",
                    deviceProcessing.IMEI);

                var eztrackDevice = await connection.QueryFirstOrDefaultAsync(
                    "SELECT TOP 1 id, version FROM eztrack_device WHERE imei = @imei",
                    new { imei = deviceProcessing.IMEI },
                    transaction);

                if (eztrackDevice != null)
                {
                    DateTime? dateOfLastMoveUpdated;

                    if (deviceProcessing.IsMove == false)
                    {
                        var lastEventDateOfLastMove = await connection.QueryFirstOrDefaultAsync<DateTime?>(
                            "SELECT TOP 1 date_of_last_move FROM eztrack_event WHERE imei = @imei ORDER BY id DESC",
                            new { imei = deviceProcessing.IMEI },
                            transaction);

                        dateOfLastMoveUpdated = lastEventDateOfLastMove;
                    }
                    else
                    {
                        dateOfLastMoveUpdated = deviceProcessing.SourceTimestamp;
                    }

                    long pingSensorId = await connection.ExecuteScalarAsync<long>(
                        @"INSERT INTO ping_sensor (created, deleted, enabled, uuid, version, battery, temperature)
                          OUTPUT INSERTED.id
                          VALUES (GETUTCDATE(), 0, 1, @uuid, 1, @battery, @temperature)",
                        new
                        {
                            uuid = Guid.NewGuid().ToString(),
                            battery = (float)deviceProcessing.Battery,
                            temperature = deviceProcessing.Temperature == 0 ? (float?)null : deviceProcessing.Temperature
                        },
                        transaction);

                    long pingLocationId = await connection.ExecuteScalarAsync<long>(
                        @"INSERT INTO ping_location (created, deleted, enabled, uuid, version, latitude, longitude)
                          OUTPUT INSERTED.id
                          VALUES (GETUTCDATE(), 0, 1, @uuid, 1, @latitude, @longitude)",
                        new
                        {
                            uuid = Guid.NewGuid().ToString(),
                            latitude = deviceProcessing.Latitude,
                            longitude = deviceProcessing.Longitude
                        },
                        transaction);

                    string assetName = await connection.QueryFirstOrDefaultAsync<string>(
                        "SELECT TOP 1 asset_id FROM asset WHERE uuid = @uuid",
                        new { uuid = deviceProcessing.AssetUuid },
                        transaction);

                    long eztrackEventId = await connection.ExecuteScalarAsync<long>(
                        @"INSERT INTO eztrack_event (
                            created, deleted, enabled, updated, uuid, version,
                            address, asset_uuid, city, date_of_last_move, direction,
                            distance_from_domicile_in_meters, distance_from_previous_event_in_meters,
                            first_move_of_day, fuel, geofence_address, geofence_city, geofence_postal,
                            geofence_state, idle_time, imei, is_move, location_name, mileage,
                            move_threshold_in_meters, moves_in_last30days, postal, sequence,
                            source_timestamp, speed, state, location_id, sensors_id, zone,
                            asset_domicile_name, asset_name, tracker_type,
                            dwell_time_start, dwell_time, excrusion_time_start, excrusion_time)
                          OUTPUT INSERTED.id
                          VALUES (
                            GETUTCDATE(), 0, 1, GETUTCDATE(), @uuid, 1,
                            @address, @assetUuid, @city, @dateOfLastMove, @direction,
                            @distanceFromDomicile, @distanceFromPreviousEvent,
                            @firstMoveOfDay, @fuel, @geofenceAddress, @geofenceCity, @geofencePostal,
                            @geofenceState, @idleTime, @imei, @isMove, @locationName, 0,
                            804.67, @movesInLast30days, @postal, 0,
                            @sourceTimestamp, @speed, @state, @locationId, @sensorsId, @zone,
                            @assetDomicile, @assetName, @trackerType,
                            @dwellTimeStart, @dwellTime, @excrusionTimeStart, @excrusionTime)",
                        new
                        {
                            uuid = Guid.NewGuid().ToString(),
                            address = deviceProcessing.Address,
                            assetUuid = deviceProcessing.AssetUuid,
                            city = deviceProcessing.City,
                            dateOfLastMove = dateOfLastMoveUpdated,
                            direction = deviceProcessing.Direction,
                            distanceFromDomicile = deviceProcessing.DistanceFromDomicile,
                            distanceFromPreviousEvent = deviceProcessing.DistanceFromPreviousEvent,
                            firstMoveOfDay = deviceProcessing.FirstMoveOfDay,
                            fuel = deviceProcessing.Fuel,
                            geofenceAddress = deviceProcessing.GeofenceAddress,
                            geofenceCity = deviceProcessing.GeofenceCity,
                            geofencePostal = deviceProcessing.GeofencePostal,
                            geofenceState = deviceProcessing.GeofenceState,
                            idleTime = deviceProcessing.IdleTime,
                            imei = deviceProcessing.IMEI,
                            isMove = deviceProcessing.IsMove ? 1 : 0,
                            locationName = deviceProcessing.LocationName,
                            movesInLast30days = deviceProcessing.Thirty,
                            postal = deviceProcessing.Postal,
                            sourceTimestamp = deviceProcessing.SourceTimestamp,
                            speed = deviceProcessing.Speed,
                            state = deviceProcessing.State,
                            locationId = pingLocationId,
                            sensorsId = pingSensorId,
                            zone = deviceProcessing.zone,
                            assetDomicile = deviceProcessing.AssetDomicile,
                            assetName,
                            trackerType = deviceProcessing.TrackerType ?? "WiFiCellular",
                            dwellTimeStart = deviceProcessing.DwellTime?.DwellTimeStart,
                            dwellTime = deviceProcessing.DwellTime?.Days,
                            excrusionTimeStart = deviceProcessing.ExcursionTime?.ExcursionTimeStart,
                            excrusionTime = deviceProcessing.ExcursionTime?.Days
                        },
                        transaction);

                    var eventDates = await connection.QueryFirstOrDefaultAsync(
                        "SELECT MIN(created) AS StartDate, MAX(created) AS EndDate FROM eztrack_event WHERE imei = @imei",
                        new { imei = deviceProcessing.IMEI },
                        transaction);

                    long daysOfEventHistory = 0;
                    if (eventDates != null && eventDates.StartDate != null && eventDates.EndDate != null)
                    {
                        daysOfEventHistory = ((DateTime)eventDates.EndDate - (DateTime)eventDates.StartDate).Days;
                    }

                    string latestAddress = string.IsNullOrEmpty(deviceProcessing.GeofenceAddress) ? deviceProcessing.Address : deviceProcessing.GeofenceAddress;
                    string latestCity = string.IsNullOrEmpty(deviceProcessing.GeofenceCity) ? deviceProcessing.City : deviceProcessing.GeofenceCity;
                    string latestState = string.IsNullOrEmpty(deviceProcessing.GeofenceState) ? deviceProcessing.State : deviceProcessing.GeofenceState;
                    string latestPostal = string.IsNullOrEmpty(deviceProcessing.GeofencePostal) ? deviceProcessing.Postal : deviceProcessing.GeofencePostal;

                    await connection.ExecuteAsync(
                        @"UPDATE eztrack_device SET
                            updated = GETUTCDATE(),
                            version = version + 1,
                            tracker_type = @trackerType,
                            owner_id = @ownerId,
                            battery = @battery,
                            last_event_latitude = @latitude,
                            last_event_longitude = @longitude,
                            distance_from_domicile_in_meters = @distanceFromDomicile,
                            asset_id = @assetId,
                            latest_event_address = @latestAddress,
                            latest_event_city = @latestCity,
                            latest_event_state = @latestState,
                            latest_event_postal = @latestPostal,
                            days_of_event_history = @daysOfEventHistory,
                            latest_event_date = @sourceTimestamp,
                            location_name = @locationName,
                            dwell_time_start = @dwellTimeStart,
                            excrusion_time_start = @excrusionTimeStart,
                            date_of_last_move = @dateOfLastMove
                          WHERE imei = @imei",
                        new
                        {
                            trackerType = deviceProcessing.TrackerType,
                            ownerId = deviceProcessing.CompanyId,
                            battery = deviceProcessing.Battery,
                            latitude = deviceProcessing.Latitude,
                            longitude = deviceProcessing.Longitude,
                            distanceFromDomicile = deviceProcessing.DistanceFromDomicile,
                            assetId = deviceProcessing.AssetId,
                            latestAddress,
                            latestCity,
                            latestState,
                            latestPostal,
                            daysOfEventHistory,
                            sourceTimestamp = deviceProcessing.SourceTimestamp,
                            locationName = deviceProcessing.LocationName,
                            dwellTimeStart = deviceProcessing.DwellTime?.DwellTimeStart,
                            excrusionTimeStart = deviceProcessing.ExcursionTime?.ExcursionTimeStart,
                            dateOfLastMove = dateOfLastMoveUpdated,
                            imei = deviceProcessing.IMEI
                        },
                        transaction);

                    await connection.ExecuteAsync(
                        @"UPDATE asset SET
                            updated = GETUTCDATE(),
                            version = version + 1,
                            company_id = @companyId,
                            battery = @battery,
                            last_event_latitude = @latitude,
                            last_event_longitude = @longitude,
                            distance_from_domicile_in_meters = @distanceFromDomicile,
                            latest_event_address = @latestAddress,
                            latest_event_city = @latestCity,
                            latest_event_state = @latestState,
                            latest_event_postal = @latestPostal,
                            days_of_event_history = @daysOfEventHistory,
                            latest_event_date = @sourceTimestamp,
                            moves_in_last3days = @three,
                            moves_in_last7days = @seven,
                            moves_in_last30days = @thirty,
                            moves_in_last60days = @sixty,
                            moves_in_last90days = @ninety,
                            temperature_inc = @temperatureInc,
                            location_name = @locationName,
                            date_of_last_move = @dateOfLastMove
                          WHERE uuid = @assetUuid",
                        new
                        {
                            companyId = deviceProcessing.CompanyId,
                            battery = deviceProcessing.Battery,
                            latitude = deviceProcessing.Latitude,
                            longitude = deviceProcessing.Longitude,
                            distanceFromDomicile = deviceProcessing.DistanceFromDomicile,
                            latestAddress,
                            latestCity,
                            latestState,
                            latestPostal,
                            daysOfEventHistory,
                            sourceTimestamp = deviceProcessing.SourceTimestamp,
                            three = deviceProcessing.Three,
                            seven = deviceProcessing.Seven,
                            thirty = deviceProcessing.Thirty,
                            sixty = deviceProcessing.Sixty,
                            ninety = deviceProcessing.Ninety,
                            temperatureInc = deviceProcessing.Temperature == 0 ? (float?)null : deviceProcessing.Temperature,
                            locationName = deviceProcessing.LocationName,
                            dateOfLastMove = dateOfLastMoveUpdated,
                            assetUuid = deviceProcessing.AssetUuid
                        },
                        transaction);

                    await connection.ExecuteAsync(
                        "INSERT INTO eztrack_device_events (eztrack_device_id, events_id) VALUES (@eztrackDeviceId, @eventsId)",
                        new { eztrackDeviceId = (long)eztrackDevice.id, eventsId = eztrackEventId },
                        transaction);

                    await transaction.CommitAsync(ct);
                }
                else
                {
                    _logger.LogWarning("Device with IMEI: {Imei} not found", deviceProcessing.IMEI);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[Device Processing] SQL Exception for IMEI: {Imei}", deviceProcessing.IMEI);
                await transaction.RollbackAsync(ct);
                throw;
            }
            finally
            {
                watch.Stop();
                _logger.LogWarning(
                    "Device Processing ended — Execution Time: {ElapsedMs}ms {ElapsedSec}s",
                    watch.ElapsedMilliseconds, watch.Elapsed.Seconds);
            }
        }
    }
}



using System.Collections.Generic;
using System;
using e4scoreDataIngestionFunctionApp.Models.DomainModels;
using System.Data;
using e4scoreDataIngestionFunctionApp.Helpers.Extensions;
using e4scoreDataIngestionFunctionApp.Models.Enum;
using e4scoreDataIngestionFunctionApp.Models.RequestModels;
using e4scoreDataIngestionFunctionApp.Models.GeoLocation;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using e4scoreDataIngestionFunctionApp.Models;
using e4scoreDataIngestionFunctionApp.Services;
using e4scoreDataIngestionFunctionApp.Interfaces;
using System.Diagnostics;
using Dapper;
using Microsoft.Data.SqlClient;

namespace e4scoreDataIngestionFunctionApp.DataAccess
{
    public class MySQLDatabase : IMySQLDatabase
    {
        private readonly IAzureRedisCache _azureRedisCache;
        private readonly string _connectionString;

        public MySQLDatabase(IAzureRedisCache azureRedisCache)
        {
            _azureRedisCache = azureRedisCache;
            _connectionString = Environment.GetEnvironmentVariable(ApplicationSettings.SqlConnection);
        }

        public DeviceEvent GetDeviceEventsByIMEI(MatrackRequest matrackRequest, ILogger log)
        {
            try
            {
                Models.DomainModels.Event events = new Models.DomainModels.Event();
                LastMovesInNDays lastMovesInNDays = new LastMovesInNDays();

                using var connection = new SqlConnection(_connectionString);

                bool findImei = connection.ExecuteScalar<bool>(
                    "SELECT CASE WHEN EXISTS (SELECT 1 FROM eztrack_device WHERE imei = @imei AND asset_id IS NOT NULL) THEN 1 ELSE 0 END",
                    new { imei = matrackRequest.imei });

                if (!findImei)
                {
                    SaveUnIdentifiedDevices(matrackRequest);
                    return new DeviceEvent { DeviceNotFound = true };
                }

                var device = GetDeviceData(matrackRequest, connection);

                if (device != null)
                {
                    events.imei = device.Imei;
                    events.company_id = (long)device.OwnerId;
                    events.date_of_last_move = matrackRequest.timestamp;
                    events.assetid = device.AssetId;
                    events.last_latitude = device.LastEventLatitude;
                    events.last_longitude = device.LastEventLongitude;
                    events.tracker_type = device.TrackerType;
                    events.latitude = device.Latitude;
                    events.longitude = device.Longitude;
                    events.domicile_name1 = device.Name;
                    events.asset_domicile_name = device.Name;
                    events.asset_name1 = device.asset_name;
                    events.asset_uuid = device.Uuid;
                    events.moves_in_last3days = device.MovesInLast3days;
                    events.moves_in_last7days = device.MovesInLast7days;
                    events.moves_in_last30days = device.MovesInLast30days;
                    events.moves_in_last60days = device.MovesInLast60days;
                    events.moves_in_last90days = device.MovesInLast90days;
                    events.zone = device.Zone;
                    events.tracker_type = device.TrackerType;
                    events.source_timestamp = device.SourceTimestamp;
                }

                if (events != null)
                {
                    lastMovesInNDays.three = events.moves_in_last3days is null ? 0 : (int)events.moves_in_last3days;
                    lastMovesInNDays.seven = events.moves_in_last7days is null ? 0 : (int)events.moves_in_last7days;
                    lastMovesInNDays.thirty = events.moves_in_last30days is null ? 0 : (int)events.moves_in_last30days;
                    lastMovesInNDays.sixty = events.moves_in_last60days is null ? 0 : (int)events.moves_in_last60days;
                    lastMovesInNDays.ninety = events.moves_in_last90days is null ? 0 : (int)events.moves_in_last90days;
                    lastMovesInNDays.source_timestamp = matrackRequest?.timestamp;
                }

                if (!events.imei.Any())
                {
                    log.LogInformation($"Device with imei : {matrackRequest.imei} not found #######################");
                    return new DeviceEvent { EventNotFound = true };
                }
                return new DeviceEvent { Event = events, lastMovesInNDays = lastMovesInNDays };
            }
            catch (Exception ex)
            {
                log.LogInformation($"[GetDeviceEvents] SQL Exception : {ex.Message} --------------------------------");
                throw;
            }
        }

        public AddressInfo GetGeocodeLocation(MatrackRequest matrackRequest, ILogger log)
        {
            try
            {
                var watch = new Stopwatch();
                watch.Start();
                string latStr = matrackRequest.location.primary.latitude.ToString("0.000");
                string langStr = matrackRequest.location.primary.longitude.ToString("0.000");
                if (latStr.Length < 6 || langStr.Length < 7)
                {
                    return null;
                }

                using var connection = new SqlConnection(_connectionString);

                const string exactSql = @"
                    SELECT id, latitude, longitude,
                           street_address AS StreetAddress, locality AS Locality,
                           state, country, postal, full_location AS FullLocation, created, version
                    FROM geocode_location
                    WHERE longitude = @longitude AND latitude = @latitude";

                var geocodeLocation = connection.QueryFirstOrDefault<Models.GeocodeLocation>(
                    exactSql, new { longitude = langStr, latitude = latStr });

                if (geocodeLocation is null)
                {
                    const string likeSql = @"
                        SELECT id, latitude, longitude,
                               street_address AS StreetAddress, locality AS Locality,
                               state, country, postal, full_location AS FullLocation, created, version
                        FROM geocode_location
                        WHERE longitude LIKE '%' + @longitude + '%' AND latitude LIKE '%' + @latitude + '%'";

                    geocodeLocation = connection.QueryFirstOrDefault<Models.GeocodeLocation>(
                        likeSql, new { longitude = langStr, latitude = latStr });
                }

                if (geocodeLocation == null)
                {
                    return null;
                }

                var addressInfo = new AddressInfo
                {
                    Address = geocodeLocation.StreetAddress,
                    Postal = geocodeLocation.Postal,
                    City = geocodeLocation.Locality,
                    State = geocodeLocation.State,
                    Country = geocodeLocation.Country
                };
                watch.Stop();
                log.LogWarning($"Get Geolocation from Database : {geocodeLocation.StreetAddress} Time : {watch.Elapsed.Seconds} sec ");
                return addressInfo;
            }
            catch (Exception ex)
            {
                log.LogInformation($"[GetGeocodeLocation] SQL Exception : {ex.Message} --------------------------------");
                throw;
            }
        }

        public void SaveGeocodeLocation(MatrackRequest matrackRequest, AddressInfo addressInfo, ILogger log)
        {
            try
            {
                string longitude = matrackRequest.location.primary.longitude.ToString("0.000");
                string latitude = matrackRequest.location.primary.latitude.ToString("0.000");

                using var connection = new SqlConnection(_connectionString);

                bool exists = connection.ExecuteScalar<bool>(
                    "SELECT CASE WHEN EXISTS (SELECT 1 FROM geocode_location WHERE longitude = @longitude AND latitude = @latitude) THEN 1 ELSE 0 END",
                    new { longitude, latitude });

                if (!exists)
                {
                    const string insertSql = @"
                        INSERT INTO geocode_location (created, version, latitude, longitude, street_address, locality, state, country, postal, full_location)
                        VALUES (GETUTCDATE(), 0, @latitude, @longitude, @streetAddress, @locality, @state, @country, @postal, @fullLocation)";

                    connection.Execute(insertSql, new
                    {
                        latitude,
                        longitude,
                        streetAddress = string.IsNullOrEmpty(addressInfo.Address) ? null : addressInfo.Address,
                        locality = addressInfo.City,
                        state = addressInfo.State,
                        country = addressInfo.Country,
                        postal = addressInfo.Postal,
                        fullLocation = addressInfo.FullLocation
                    });
                }
            }
            catch (Exception ex)
            {
                log.LogInformation($"[SaveGeocodeLocation] SQL Exception : {ex.Message} --------------------------------");
                throw;
            }
        }

        private void SaveUnIdentifiedDevices(MatrackRequest matrackRequest)
        {
            string latitude = Convert.ToString(matrackRequest.location.primary.latitude);
            string longitude = Convert.ToString(matrackRequest.location.primary.longitude);

            using var connection = new SqlConnection(_connectionString);

            const string sql = @"
                INSERT INTO unidentified_device_data (imei, longitude, latitude, source_timestamp, created, device_provider)
                VALUES (@imei, @longitude, @latitude, @sourceTimestamp, GETUTCDATE(), @deviceProvider)";

            connection.Execute(sql, new
            {
                imei = matrackRequest.imei,
                longitude,
                latitude,
                sourceTimestamp = matrackRequest.timestamp,
                deviceProvider = matrackRequest.location.located_with
            });
        }

        private dynamic GetDeviceData(MatrackRequest matrackRequest, IDbConnection connection)
        {
            const string sql = @"
                SELECT
                    d.imei          AS Imei,
                    d.owner_id      AS OwnerId,
                    d.date_of_last_move AS DateOfLastMove,
                    d.asset_id      AS AssetId,
                    d.last_event_latitude  AS LastEventLatitude,
                    d.last_event_longitude AS LastEventLongitude,
                    d.tracker_type  AS TrackerType,
                    l.latitude      AS Latitude,
                    l.longitude     AS Longitude,
                    l.name          AS Name,
                    b.asset_id      AS asset_name,
                    b.uuid          AS Uuid,
                    d.moves_in_last3days  AS MovesInLast3days,
                    d.moves_in_last7days  AS MovesInLast7days,
                    d.moves_in_last30days AS MovesInLast30days,
                    d.moves_in_last60days AS MovesInLast60days,
                    d.moves_in_last90days AS MovesInLast90days,
                    d.zone          AS Zone,
                    d.latest_event_date AS SourceTimestamp
                FROM eztrack_device d
                INNER JOIN asset b ON d.asset_id = b.id
                INNER JOIN location l ON b.domicile_id = l.id
                WHERE d.imei = @imei";

            return connection.QueryFirstOrDefault(sql, new { imei = matrackRequest.imei });
        }

        private dynamic GetEventsData(MatrackRequest matrackRequest, IDbConnection connection)
        {
            const string sql = @"
                SELECT TOP 1
                    b.uuid          AS Uuid,
                    a.id            AS Id,
                    b.asset_id      AS asset_name,
                    b.id            AS assetid,
                    b.moves_in_last3days  AS MovesInLast3days,
                    b.moves_in_last7days  AS MovesInLast7days,
                    b.moves_in_last30days AS MovesInLast30days,
                    b.moves_in_last60days AS MovesInLast60days,
                    b.moves_in_last90days AS MovesInLast90days,
                    l.latitude      AS Latitude,
                    l.longitude     AS Longitude,
                    l.name          AS Name,
                    l.company_id    AS CompanyId,
                    p.longitude     AS last_longitude,
                    p.latitude      AS last_latitude,
                    a.imei          AS Imei,
                    a.tracker_type  AS TrackerType,
                    a.asset_uuid    AS AssetUuid,
                    a.is_move       AS IsMove,
                    a.first_move_of_day AS FirstMoveOfDay,
                    a.date_of_last_move AS DateOfLastMove,
                    a.zone          AS Zone,
                    a.excrusion_time       AS ExcrusionTime,
                    a.excrusion_time_start AS ExcrusionTimeStart,
                    a.dwell_time           AS DwellTime,
                    a.dwell_time_start     AS DwellTimeStart,
                    a.source_timestamp     AS SourceTimestamp
                FROM eztrack_event a
                INNER JOIN ping_location p ON a.location_id = p.id
                INNER JOIN asset b ON a.asset_uuid = b.uuid
                INNER JOIN location l ON b.domicile_id = l.id
                WHERE a.imei = @imei
                ORDER BY a.id DESC";

            return connection.QueryFirstOrDefault(sql, new { imei = matrackRequest.imei });
        }
    }
}

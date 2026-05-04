using e4scoreDataIngestionFunctionApp.Models;
using e4scoreDataIngestionFunctionApp.Models.DomainModels;
using e4scoreDataIngestionFunctionApp.Models.Enum;
using e4scoreDataIngestionFunctionApp.Models.GeoLocation;
using e4scoreDataIngestionFunctionApp.Models.Redis;
using e4scoreDataIngestionFunctionApp.Models.RequestModels;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace e4scoreDataIngestionFunctionApp.DataAccess
{
    public class AzureRedisCache : IAzureRedisCache
    {
        private readonly Lazy<ConnectionMultiplexer> _lazyRedisConnection = new Lazy<ConnectionMultiplexer>(() =>
        {
            string cacheConnection = Environment.GetEnvironmentVariable(ApplicationSettings.AzureRedisConnection)
                ?? throw new InvalidOperationException($"Environment variable '{ApplicationSettings.AzureRedisConnection}' is not set.");
            var options = ConfigurationOptions.Parse(cacheConnection);
            options.SyncTimeout = 10000;
            return ConnectionMultiplexer.Connect(options);
        });

        public ConnectionMultiplexer RedisConnection => _lazyRedisConnection.Value;

        private IDatabase Connect() => RedisConnection.GetDatabase();

        #region Device
        public void SetDeviceCache(Device device)
        {
            Connect().StringSet(RedisKeys.DeviceKey + device.imei, JsonSerializer.Serialize(device), RedisKeys.ReferenceDataTtl);
        }

        public Device GetDeviceCache(string imei)
        {
            string value = Connect().StringGet(RedisKeys.DeviceKey + imei);
            return value is not null ? JsonSerializer.Deserialize<Device>(value) : null;
        }
        #endregion

        #region Unidentified Devices
        public void SetUnidentifiedCache(string imei)
        {
            if (!UnidentifiedCacheExists(imei))
                Connect().StringSet(RedisKeys.UnidentifiedDeviceKey + imei, imei, RedisKeys.UnidentifiedDeviceTtl);
        }

        public bool UnidentifiedCacheExists(string imei)
        {
            string value = Connect().StringGet(RedisKeys.UnidentifiedDeviceKey + imei);
            return !string.IsNullOrEmpty(value);
        }
        #endregion

        #region Device Runtime (last telemetry)
        public void SetDeviceRuntimeCache(MatrackRequest request)
        {
            Connect().StringSet(RedisKeys.DeviceRuntimeKey + request.imei, JsonSerializer.Serialize(request), RedisKeys.RuntimeStateTtl);
        }

        public MatrackRequest GetDeviceRuntimeCache(string imei)
        {
            string value = Connect().StringGet(RedisKeys.DeviceRuntimeKey + imei.Trim());
            return value is not null ? JsonSerializer.Deserialize<MatrackRequest>(value) : null;
        }
        #endregion

        #region Date Of Last Move
        public void SetDateOfLastMoveCache(DateTime dateOfLastMove, string imei)
        {
            Connect().StringSet(RedisKeys.DateOfLastMoveKey + imei, Convert.ToString(dateOfLastMove), RedisKeys.RuntimeStateTtl);
        }

        public string GetDateOfLastMoveCache(string imei)
        {
            string value = Connect().StringGet(RedisKeys.DateOfLastMoveKey + imei);
            return value;
        }
        #endregion

        #region Event
        public void SetEventCache(Event deviceEvent)
        {
            if (deviceEvent is null) return;
            Connect().StringSet(RedisKeys.EventKey + deviceEvent.imei, JsonSerializer.Serialize(deviceEvent), RedisKeys.RuntimeStateTtl);
        }

        public Event GetEventCache(string imei)
        {
            string value = Connect().StringGet(RedisKeys.EventKey + imei);
            return value is not null ? JsonSerializer.Deserialize<Event>(value) : null;
        }
        #endregion

        #region Moves In Last N Days
        public void SetMovesInLastNDaysCache(LastMovesInNDays lastMovesInNDays, string imei)
        {
            if (lastMovesInNDays is null) return;
            Connect().StringSet(RedisKeys.MovesInNDaysKey + imei, JsonSerializer.Serialize(lastMovesInNDays), RedisKeys.RuntimeStateTtl);
        }

        public LastMovesInNDays GetMovesInLastNDaysCache(string imei)
        {
            string value = Connect().StringGet(RedisKeys.MovesInNDaysKey + imei);
            return value is not null ? JsonSerializer.Deserialize<LastMovesInNDays>(value) : null;
        }
        #endregion

        #region Dwell Time
        public void SetDwellTimeCache(DwellTime dwellTime)
        {
            Connect().StringSet(RedisKeys.DwellTimeKey + dwellTime.Imei, JsonSerializer.Serialize(dwellTime), RedisKeys.RuntimeStateTtl);
        }

        public DwellTime GetDwellTimeCache(string imei)
        {
            string value = Connect().StringGet(RedisKeys.DwellTimeKey + imei);
            return value is not null ? JsonSerializer.Deserialize<DwellTime>(value) : null;
        }
        #endregion

        #region Excursion Time
        public void SetExcursionTimeCache(ExcursionTime excursionTime)
        {
            Connect().StringSet(RedisKeys.ExcursionTimeKey + excursionTime.Imei, JsonSerializer.Serialize(excursionTime), RedisKeys.RuntimeStateTtl);
        }

        public ExcursionTime GetExcursionTimeCache(string imei)
        {
            string value = Connect().StringGet(RedisKeys.ExcursionTimeKey + imei);
            return value is not null ? JsonSerializer.Deserialize<ExcursionTime>(value) : null;
        }
        #endregion

        #region Asset Profile
        public void SetAssetProfileCache(string imei, DeviceDwellTime deviceDwellTime, DeviceExcursionTime deviceExcursionTime, long idleTime)
        {
            var profile = new AssetProfile
            {
                DeviceDwellTime = deviceDwellTime,
                DeviceExcursionTime = deviceExcursionTime,
                IdleTime = idleTime
            };
            Connect().StringSet(RedisKeys.AssetProfileKey + imei, JsonSerializer.Serialize(profile), RedisKeys.RuntimeStateTtl);
        }

        public AssetProfile GetAssetProfileCache(string imei)
        {
            string value = Connect().StringGet(RedisKeys.AssetProfileKey + imei);
            return value is not null ? JsonSerializer.Deserialize<AssetProfile>(value) : null;
        }
        #endregion

        #region Geofence Locations
        public HashEntry[] GetAllLocationsByCompanyIdCache(string companyId)
        {
            return Connect().HashGetAll(RedisKeys.LocationsKey + companyId);
        }
        #endregion
    }
}


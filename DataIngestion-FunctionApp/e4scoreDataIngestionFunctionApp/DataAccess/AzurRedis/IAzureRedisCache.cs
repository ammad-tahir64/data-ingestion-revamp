using e4scoreDataIngestionFunctionApp.Models;
using e4scoreDataIngestionFunctionApp.Models.DomainModels;
using e4scoreDataIngestionFunctionApp.Models.GeoLocation;
using e4scoreDataIngestionFunctionApp.Models.Redis;
using e4scoreDataIngestionFunctionApp.Models.RequestModels;
using StackExchange.Redis;
using System;
using System.Threading.Tasks;

namespace e4scoreDataIngestionFunctionApp.DataAccess
{
    public interface IAzureRedisCache
    {
        ConnectionMultiplexer RedisConnection { get; }

        // Device reference
        Device GetDeviceCache(string imei);
        void SetDeviceCache(Device device);

        // Unidentified device marker
        void SetUnidentifiedCache(string imei);
        bool UnidentifiedCacheExists(string imei);

        // Device runtime (last telemetry)
        void SetDeviceRuntimeCache(MatrackRequest request);
        MatrackRequest GetDeviceRuntimeCache(string imei);

        // Date of last move
        void SetDateOfLastMoveCache(DateTime dateOfLastMove, string imei);
        string GetDateOfLastMoveCache(string imei);

        // Last eztrack event
        void SetEventCache(Event deviceEvent);
        Event GetEventCache(string imei);

        // Moves in last N days
        void SetMovesInLastNDaysCache(LastMovesInNDays lastMovesInNDays, string imei);
        LastMovesInNDays GetMovesInLastNDaysCache(string imei);

        // Dwell time
        void SetDwellTimeCache(DwellTime dwellTime);
        DwellTime GetDwellTimeCache(string imei);

        // Excursion time
        void SetExcursionTimeCache(ExcursionTime excursionTime);
        ExcursionTime GetExcursionTimeCache(string imei);

        // Asset profile
        void SetAssetProfileCache(string imei, DeviceDwellTime deviceDwellTime, DeviceExcursionTime deviceExcursionTime, long idleTime);
        AssetProfile GetAssetProfileCache(string imei);

        // Geofence locations
        HashEntry[] GetAllLocationsByCompanyIdCache(string companyId);
    }
}


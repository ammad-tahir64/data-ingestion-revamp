using e4scoreDataIngestionFunctionApp.Models;
using e4scoreDataIngestionFunctionApp.Models.DomainModels;
using e4scoreDataIngestionFunctionApp.Models.GeoLocation;
using e4scoreDataIngestionFunctionApp.Models.Redis;
using e4scoreDataIngestionFunctionApp.Models.RequestModels;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace e4scoreDataIngestionFunctionApp.DataAccess
{
    public interface IAzureRedisCache
    {
        ConnectionMultiplexer RedisConnection { get; }

        Event GetEventCache(string imei);

        Device GetDeviceCache(string imei);

        void SetDeviceCache(Device device);

        void SetEventCache(Event events);

        MatrackRequest GetMatrackCache(string imei);

        void SetDateOfLastMoveCache(DateTime dateOfLastMove, string imei);

        void updateMovesInLastNDaysCache(List<LastMoveDays> lastMoveDays, MatrackRequest matrackRequest, ulong isMove);

        void SetMovesInLastNDaysCache(List<LastMoveDays> lastMoveDays, string imei);
        void SetMovesInLastNDaysCacheFromEvent(LastMovesInNDays lastMovesInNDays, string imei);

        List<LastMoveDays> GetMovesInLastNDaysCache(string imei);
    LastMovesInNDays GetMovesInLastNDaysCacheNew(string imei);

        string GetDateOfLastMoveCache(string imei);

        void SetMatrackCache(MatrackRequest device);

        void SetDwellTimeCache(DwellTime dwellTime);

        DwellTime GetDwellTimeCache(string imei);

        HashEntry[] GetAllLocationsByComapnyIdCache(string companyId);

        void SetExcursionTimeCache(ExcursionTime dwellTime);

        ExcursionTime GetExcursionTimeCache(string imei);
        void SetUnidentifiedCache(string imei);
        bool UnidentifiedCacheExists(string imei);

        void SetAssetProfileCache(string imei, DeviceDwellTime deviceDwellTime, DeviceExcursionTime deviceExcursionTime, long idleTime);

        AssetProfile GetAssetProfileCache(string imei);
    }
}

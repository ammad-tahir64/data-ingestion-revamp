using e4scoreDataIngestionFunctionApp.Models.GeoLocation;
using System;

namespace e4scoreDataIngestionFunctionApp.Interfaces
{
    public interface ICalculateExcursionTime
    {
        ExcusrionResponse GetExcursionTime(RedisLocation locationDetails, DateTime sourceTimeStamp, string imei, Models.DomainModels.Event deviceEvent);
        ExcursionTime CreateExcursionTime(string imei, string locationId, DateTime sourceTimeStamp, Models.DomainModels.Event deviceEvent);
    }
}
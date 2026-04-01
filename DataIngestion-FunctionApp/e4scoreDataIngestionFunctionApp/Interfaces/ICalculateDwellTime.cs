using e4scoreDataIngestionFunctionApp.Models.GeoLocation;
using System;
using System.Collections.Generic;

namespace e4scoreDataIngestionFunctionApp.Interfaces
{
    public interface ICalculateDwellTime
    {
        bool IsLatLongInsideRadiusWithoutShape(double currentLat, double currentLong, double cacheLat, double cacheLong, string radius);
        bool IsLatLongInsideRadiusWithShape(string shapeData, double currentLat, double currentLong);
        bool IsPointInsidePolygon(string shapeData, double currentLat, double currentLong);
        RedisLocation LocationInfoByLatLong(double currentLong, double currentLat, long companyId);
        RedisLocation MatchDecimalPlaces(List<RedisLocation> locations, double sourceLatitude, string sourceLongitude);
        DwellResponse GetDwellTime(RedisLocation lmocationDetails, DateTime sourceTimeStamp, string imei,Models.DomainModels.Event deviceEvent);
        DwellTime CreateDwellTime(string imei, string locationId, DateTime sourceTimeStamp, Models.DomainModels.Event deviceEvent);
    }
}
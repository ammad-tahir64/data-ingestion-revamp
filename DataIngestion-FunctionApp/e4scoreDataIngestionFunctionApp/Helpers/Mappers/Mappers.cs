using e4scoreDataIngestionFunctionApp.Helpers.Extensions;
using e4scoreDataIngestionFunctionApp.Models.DomainModels;
using e4scoreDataIngestionFunctionApp.Models.GeoLocation;
using e4scoreDataIngestionFunctionApp.Models.RequestModels;
using e4scoreDataIngestionFunctionApp.Models;
using System;

namespace e4scoreDataIngestionFunctionApp.Helpers.Mappers
{
    public static class Mappers
    {/// <summary>
     /// Maps calculated fields into a consolidated model for saving in database
     /// </summary>
     /// <param name="matrackRequest"></param>
     /// <param name="deviceEvent"></param>
     /// <param name="addressInfo"></param>
     /// <param name="first_move_of_day"></param>
     /// <param name="is_move"></param>
     /// <param name="distanceFromPreviousEvent"></param>
     /// <param name="idleTime"></param>
     /// <param name="movesInNDays"></param>
     /// <param name="dwellTime"></param>
     /// <param name="locationDetails"></param>
     /// <param name="excursionTime"></param>
     /// <returns>consolidated model of DeviceProcessing</returns>
        public static DeviceProcessing DeviceInfoToDeviceProcessing(MatrackRequest matrackRequest, Models.DomainModels.Event deviceEvent, AddressInfo addressInfo, int first_move_of_day, int is_move, double distanceFromPreviousEvent, long idleTime, LastMovesInNDays movesInNDays, DwellResponse dwellTime, RedisLocation locationDetails, ExcusrionResponse excursionTime)
        {
            DeviceProcessing deviceProcessing = new DeviceProcessing();
            deviceProcessing.Speed = matrackRequest.sensors?.speed ?? 0;
            deviceProcessing.SourceTimestamp = matrackRequest.timestamp;
            deviceProcessing.Direction = matrackRequest.sensors?.direction;
            deviceProcessing.IMEI = matrackRequest.imei;
            deviceProcessing.EventType = matrackRequest.event_type;
            deviceProcessing.Fuel = matrackRequest.sensors?.fuel ?? 0;
            deviceProcessing.Battery = matrackRequest.sensors?.battery ?? 0;
            deviceProcessing.Temperature = matrackRequest.sensors?.temperature ?? null;
            deviceProcessing.Longitude = matrackRequest.location.primary.longitude;
            deviceProcessing.Latitude = matrackRequest.location.primary.latitude;

            deviceProcessing.AssetUuid = deviceEvent.asset_uuid;
            deviceProcessing.CompanyId = deviceEvent.company_id;
            deviceProcessing.AssetName = deviceEvent.asset_name1;
            deviceProcessing.AssetId = deviceEvent.assetid;
            deviceProcessing.AssetDomicile = deviceEvent.asset_domicile_name;
            deviceProcessing.zone = deviceEvent.zone;


            if (!string.IsNullOrEmpty(matrackRequest?.location?.located_with))
            {
                if (matrackRequest.location.located_with.Contains("gps".ToLower()))
                {
                    deviceProcessing.TrackerType = "GPS";
                }
                else if (matrackRequest.location.located_with.Contains("wifi".ToLower()))
                {
                    deviceProcessing.TrackerType = "WiFiCellular";

                }
                else if (matrackRequest.location.located_with.Contains("samsara".ToLower()))
                {
                    deviceProcessing.TrackerType = "Samsara";

                }
                else if (matrackRequest.location.located_with.Contains("roadready".ToLower()))
                {
                    deviceProcessing.TrackerType = "RoadReady";

                }
            }
            else 
            {
                deviceProcessing.TrackerType = deviceProcessing.TrackerType;
            }

            deviceProcessing.Address = addressInfo?.Address;
            deviceProcessing.City = addressInfo?.City;
            deviceProcessing.Postal = addressInfo?.Postal;
            deviceProcessing.State = addressInfo?.State;

            deviceProcessing.LocationId = string.IsNullOrEmpty(locationDetails?.Id) ? null : Convert.ToInt32(locationDetails?.Id);
            deviceProcessing.LocationName = locationDetails?.Name;
            deviceProcessing.GeofenceAddress = locationDetails?.Address1;
            deviceProcessing.GeofenceCity = locationDetails?.City;
            deviceProcessing.GeofencePostal = locationDetails?.Postal;
            deviceProcessing.GeofenceState = locationDetails?.State;

            deviceProcessing.Thirty = movesInNDays.thirty is null ? 0 : (int)movesInNDays.thirty;
            deviceProcessing.Sixty = movesInNDays.sixty is null ? 0 : (int)movesInNDays.sixty;
            deviceProcessing.Ninety = movesInNDays.ninety is null ? 0 : (int)movesInNDays.ninety;
            deviceProcessing.Seven = movesInNDays.seven is null ? 0 : (int)movesInNDays.seven;
            deviceProcessing.Three = movesInNDays.three is null ? 0 : (int)movesInNDays.three;
            deviceProcessing.IsMove = is_move == 0 ? false : true;
            DeviceDwellTime deviceDwellTime = new DeviceDwellTime
            {
                DwellTimeStart = dwellTime.DwellTimeStart,
                Days = (long)dwellTime.Days
            };
            DeviceExcursionTime deviceExcursionTime = new DeviceExcursionTime
            {
                ExcursionTimeStart = excursionTime.ExcusrionTimeStart,
                Days = (long)excursionTime.Days
            };

            deviceProcessing.DwellTime = deviceDwellTime;
            deviceProcessing.ExcursionTime = deviceExcursionTime;

            deviceProcessing.LocationUuid = Guid.NewGuid().ToString();
            deviceProcessing.SensorUuid = Guid.NewGuid().ToString();
            deviceProcessing.FirstMoveOfDay = (ulong)first_move_of_day;
            deviceProcessing.DistanceFromPreviousEvent = distanceFromPreviousEvent;
            deviceProcessing.IdleTime = idleTime;

            double distanceFromDomicile = GeoDistance.Distance(Convert.ToDouble(deviceEvent.latitude), Convert.ToDouble(deviceEvent.longitude), matrackRequest.location.primary.latitude, matrackRequest.location.primary.longitude);
            deviceProcessing.DistanceFromDomicile = distanceFromDomicile;
            return deviceProcessing;


        }
    }
}

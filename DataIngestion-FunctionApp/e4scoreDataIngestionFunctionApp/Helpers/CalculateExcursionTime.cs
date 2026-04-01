using e4scoreDataIngestionFunctionApp.DataAccess;
using e4scoreDataIngestionFunctionApp.Interfaces;
using e4scoreDataIngestionFunctionApp.Models.GeoLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e4scoreDataIngestionFunctionApp.Helpers
{
    public class CalculateExcursionTime : ICalculateExcursionTime
    {
        private readonly IAzureRedisCache _azureRedisCache;
        public CalculateExcursionTime(IAzureRedisCache azureRedisCache)
        {
            _azureRedisCache = azureRedisCache;
        }
        public ExcusrionResponse GetExcursionTime(RedisLocation locationDetails, DateTime sourceTimeStamp, string imei, Models.DomainModels.Event deviceEvent)
        {
            ExcusrionResponse excusrionResponse = new ExcusrionResponse();
            var existingExcursionTime = _azureRedisCache.GetExcursionTimeCache(imei);

            if (existingExcursionTime != null)
            {
                if (locationDetails == null)
                {
                    existingExcursionTime.InsideGeofence = false;
                    existingExcursionTime.CurrentExcursionTime = sourceTimeStamp;
                    existingExcursionTime.ExcursionTimeStart = existingExcursionTime.ExcursionTimeStart is null ?
                                                    existingExcursionTime.CurrentExcursionTime : existingExcursionTime.ExcursionTimeStart;
                    TimeSpan difference = (TimeSpan)(existingExcursionTime.CurrentExcursionTime - existingExcursionTime.ExcursionTimeStart);
                    existingExcursionTime.Days = difference.TotalDays;
                    existingExcursionTime.ExcursionTimeStop = null;
                }
                else if (locationDetails != null && locationDetails.IsDomicile)
                {
                    existingExcursionTime.InsideGeofence = true;
                    existingExcursionTime.CurrentExcursionTime = sourceTimeStamp;
                    if (existingExcursionTime.ExcursionTimeStart is not null)
                    {
                        TimeSpan difference = (TimeSpan)(existingExcursionTime.CurrentExcursionTime - existingExcursionTime.ExcursionTimeStart);
                        existingExcursionTime.Days = difference.TotalDays;
                        existingExcursionTime.ExcursionTimeStop = sourceTimeStamp;

                    }
                    else
                    {
                        existingExcursionTime.Days = 0;
                        existingExcursionTime.ExcursionTimeStop = null;
                    }

                    // existingExcursionTime.TotalExcursionTime = null;

                    existingExcursionTime.ExcursionTimeStart = null;



                }
                else if (locationDetails != null && !locationDetails.IsDomicile)
                {
                    existingExcursionTime.InsideGeofence = false;
                    existingExcursionTime.CurrentExcursionTime = sourceTimeStamp;
                    existingExcursionTime.ExcursionTimeStart = existingExcursionTime.ExcursionTimeStart is null ?
                                                    existingExcursionTime.CurrentExcursionTime : existingExcursionTime.ExcursionTimeStart;
                    TimeSpan difference = (TimeSpan)(existingExcursionTime.CurrentExcursionTime - existingExcursionTime.ExcursionTimeStart);
                    existingExcursionTime.Days = difference.TotalDays;
                }

                excusrionResponse.ExcusrionTimeStart = existingExcursionTime.ExcursionTimeStart;
                excusrionResponse.ExcusrionTimeStop = existingExcursionTime?.ExcursionTimeStop;
                excusrionResponse.InsideGeofence = existingExcursionTime.InsideGeofence;
                excusrionResponse.Days = existingExcursionTime.Days;
                _azureRedisCache.SetExcursionTimeCache(existingExcursionTime);
            }
            else
            {
                excusrionResponse.ExcusrionTimeStart = existingExcursionTime?.ExcursionTimeStart;
                excusrionResponse.ExcusrionTimeStop = existingExcursionTime?.ExcursionTimeStop;
                excusrionResponse.InsideGeofence = locationDetails is not null;
                excusrionResponse.Days = 0;
                var dwellTime = CreateExcursionTime(imei, locationDetails?.Id, sourceTimeStamp,deviceEvent);
                _azureRedisCache.SetExcursionTimeCache(dwellTime);
            }

            return excusrionResponse;

        }
        public ExcursionTime CreateExcursionTime(string imei, string locationId, DateTime sourceTimeStamp, Models.DomainModels.Event deviceEvent)
        {
            //"869842051387863"
            ExcursionTime dwellTime = new ExcursionTime();
            dwellTime.Imei = imei;
            dwellTime.LocationId = Convert.ToInt32(locationId);
            dwellTime.ExcursionTimeStart = string.IsNullOrEmpty(locationId) ? sourceTimeStamp : null;
            dwellTime.CurrentExcursionTime = sourceTimeStamp;
            dwellTime.Days = 0;
            dwellTime.ExcursionTimeStop = null;
            return dwellTime;
        }
    }
}

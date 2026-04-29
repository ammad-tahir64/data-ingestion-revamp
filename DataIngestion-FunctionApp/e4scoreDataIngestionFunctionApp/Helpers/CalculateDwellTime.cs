using e4scoreDataIngestionFunctionApp.Helpers.Extensions;
using e4scoreDataIngestionFunctionApp.Models.Polygon;
using e4scoreDataIngestionFunctionApp.Models;
using Newtonsoft.Json;
using System;
using NetTopologySuite.Geometries;
using StackExchange.Redis;
using e4scoreDataIngestionFunctionApp.Models.GeoLocation;
using System.Linq;
using e4scoreDataIngestionFunctionApp.DataAccess;
using System.Collections.Generic;
using e4scoreDataIngestionFunctionApp.Interfaces;

namespace e4scoreDataIngestionFunctionApp.Helpers
{
    public class CalculateDwellTime : ICalculateDwellTime
    {
        private readonly IAzureRedisCache _azureRedisCache;
        public CalculateDwellTime(IAzureRedisCache azureRedisCache) 
        {
            _azureRedisCache = azureRedisCache;
        }

        public DwellResponse GetDwellTime(RedisLocation locationDetails, DateTime sourceTimeStamp, string imei, Models.DomainModels.Event deviceEvent)
        {
            DwellResponse dwellResponse = new DwellResponse();
            var existingDwellTime = _azureRedisCache.GetDwellTimeCache(imei);

            if (existingDwellTime != null)
            {
                if (locationDetails != null && existingDwellTime.LocationId == Convert.ToInt32(locationDetails?.Id))
                {
                    existingDwellTime.InsideGeofence = true;
                    existingDwellTime.CurrentDwellTime = sourceTimeStamp;
                    TimeSpan difference = (TimeSpan)(existingDwellTime.CurrentDwellTime - existingDwellTime.DwellTimeStart);
                    existingDwellTime.Days = difference.TotalDays;
                }
                else if (locationDetails != null && existingDwellTime.LocationId != Convert.ToInt32(locationDetails?.Id))
                {
                    existingDwellTime.InsideGeofence = true;
                    existingDwellTime.CurrentDwellTime = sourceTimeStamp;
                    existingDwellTime.LocationId = Convert.ToInt32(locationDetails.Id);
                    existingDwellTime.DwellTimeStart = sourceTimeStamp;
                    if (existingDwellTime.DwellTimeStop != null)
                    {
                        existingDwellTime.DwellTimeStop = null;
                    }

                    TimeSpan difference = (TimeSpan)(existingDwellTime.CurrentDwellTime - existingDwellTime.DwellTimeStart);
                    existingDwellTime.Days = difference.TotalDays;
                }
                else
                {
                    existingDwellTime.InsideGeofence = false;
                    existingDwellTime.Imei = imei;
                    existingDwellTime.LocationId = 0;

                    existingDwellTime.CurrentDwellTime = sourceTimeStamp;
                    if (existingDwellTime.DwellTimeStart is not null)
                    {
                        TimeSpan difference = (TimeSpan)(existingDwellTime.CurrentDwellTime - existingDwellTime.DwellTimeStart);
                        existingDwellTime.DwellTimeStop = sourceTimeStamp;

                        existingDwellTime.Days = difference.TotalDays;
                    }
                    else
                    {
                        existingDwellTime.DwellTimeStop = null;

                        existingDwellTime.Days = 0;

                    }

                    existingDwellTime.DwellTimeStart = null;

                }
                dwellResponse.DwellTimeStart = existingDwellTime.DwellTimeStart;
                dwellResponse.DwellTimeStop = existingDwellTime.DwellTimeStop;
                dwellResponse.InsideGeofence = existingDwellTime.InsideGeofence;
                dwellResponse.InsideGeofence = existingDwellTime.InsideGeofence;
                dwellResponse.Days = existingDwellTime.Days;
                _azureRedisCache.SetDwellTimeCache(existingDwellTime);
            }
            else
            {
                dwellResponse.DwellTimeStart = locationDetails is null ? null : sourceTimeStamp;
                dwellResponse.DwellTimeStop = existingDwellTime?.DwellTimeStop;
                dwellResponse.InsideGeofence = locationDetails is not null;
                dwellResponse.Days = 0;
                var dwellTime = CreateDwellTime(imei, locationDetails?.Id, sourceTimeStamp,deviceEvent);
                _azureRedisCache.SetDwellTimeCache(dwellTime);
            }

            return dwellResponse;

        }
        public DwellTime CreateDwellTime(string imei, string locationId, DateTime sourceTimeStamp, Models.DomainModels.Event deviceEvent)
        {
            //"869842051387863"
            DwellTime dwellTime = new DwellTime();
            dwellTime.Imei = imei;
            dwellTime.LocationId = Convert.ToInt32(locationId);
            dwellTime.DwellTimeStart = sourceTimeStamp;
            dwellTime.CurrentDwellTime = sourceTimeStamp;
            dwellTime.Days = 0;
            dwellTime.DwellTimeStop = null;
            dwellTime.InsideGeofence = !string.IsNullOrEmpty(locationId);
            return dwellTime;
        }
        public  RedisLocation LocationInfoByLatLong(double currentLat, double currentLong, long companyId)
        {
            try
            {
                var shapeData = string.Empty;
                bool isInside = false;

                var locations = _azureRedisCache.GetAllLocationsByCompanyIdCache(companyId.ToString()).Select(locationCache => JsonConvert.DeserializeObject<RedisLocation>(locationCache.Value)).Where(x=>x.GeofenceType.IsNullOrEmpty() == false).ToList();

                foreach (var location in locations)
                {
                    if (location.GeofenceEnabled == null)
                    {
                        continue;

                    }
                    if (location.Id == "12901") 
                    {

                    }
                    if (location.GeofenceType.Equals("Draw on map") && location.ShapeData.Contains("Polygon"))
                    {
                        isInside = IsPointInsidePolygon(location.ShapeData, currentLat, currentLong);
                        // Console.WriteLine(isInside ? "The point is inside the polygon" : "The point is outside the polygon");
                    }
                    else if (location.GeofenceType.Equals("Draw on map") && location.ShapeData.Contains("Point"))
                    {
                        isInside = IsLatLongInsideRadiusWithShape(location.ShapeData, currentLong, currentLat);
                        // Console.WriteLine(isInside ? "The point is inside the radius" : "The point is outside the radius");
                    }
                    else if (location.GeofenceType.Equals("Specify radius") && location.GeofenceEnabled == "1" && !string.IsNullOrEmpty(location.GeofenceRadiusInMeters))
                    {
                        isInside = IsLatLongInsideRadiusWithoutShape(currentLong, currentLat, location.Latitude, location.Longitude, location.GeofenceRadiusInMeters);

                    }

                    if (isInside)
                    {
                        //Console.WriteLine($"Found -- Location Name : {location.Name} ----");
                        //Console.WriteLine($" Address : {location.Address1}  ----");
                        //Console.WriteLine($"city : {location.City}  ----");
                        //Console.WriteLine($" State : {location.State} ----");

                        return location;
                    }


                    //Console.WriteLine($"Location Id -- {location.Id}  ----");
                }
                if (!isInside)
                {
                    // var isMatch = MatchDecimalPlaces(locations);
                }
                //Console.WriteLine($"Not Found -- ----");
                return null;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public   RedisLocation MatchDecimalPlaces(List<RedisLocation> locations, double sourceLatitude, string sourceLongitude)
        {
            try
            {
                bool fiveDecimalPlaces = false;
                bool fourDecimalPlaces = false;
                bool threeDecimalPlaces = false;

                string infoString = sourceLatitude.ToString("0.00000");
                var matchingLocationWithFiveDecimalPlaces = locations.FirstOrDefault(data => (sourceLatitude == data.Latitude) && (infoString == data.Longitude.ToString("0.00000")));
                var matchingLocationWithFourDecimalPlaces = locations.FirstOrDefault(data => (sourceLatitude == data.Latitude) && (infoString == data.Longitude.ToString("0.0000")));
                var matchingLocationWithThreeeDecimalPlaces = locations.FirstOrDefault(data => (sourceLatitude == data.Latitude) && (infoString == data.Longitude.ToString("0.000")));

                if (matchingLocationWithFiveDecimalPlaces != null)
                {
                    Console.WriteLine("The values match up to five decimal places after the point.");
                    return matchingLocationWithFiveDecimalPlaces;
                }
                else
                {
                    Console.WriteLine("The values do not match up to five decimal places after the point.");

                }

                return null;
            }
            catch(Exception ex) { throw ex; }

        }
        public   bool IsLatLongInsideRadiusWithoutShape(double currentLat, double currentLong, double cacheLat, double cacheLong, string radius)
        {
            //var polygonPoints = JsonConvert.DeserializeObject<PolygonPointsRadius>(shapeData);

            double distance = GeoDistance.Distance(currentLat, currentLong, cacheLat, cacheLong);
            if (string.IsNullOrEmpty(radius))
            {
                return false;
            }

            return distance < Convert.ToDouble(radius);
        }
        public   bool IsLatLongInsideRadiusWithShape(string shapeData, double currentLat, double currentLong)
        {
            var polygonPoints = JsonConvert.DeserializeObject<PolygonPointsRadius>(shapeData);

            double distance = GeoDistance.Distance(
                Convert.ToDouble(polygonPoints.geometry.coordinates[1]),
                Convert.ToDouble(polygonPoints.geometry.coordinates[0]),
                currentLat,
                currentLong);

            return distance < polygonPoints.properties.radius;
        }
        public   bool IsPointInsidePolygon(string feature, double currentLat, double currentLong)
        {
            //using NetTopologySuite.Geometries;
            //using NetTopologySuite.IO;
            //GeoJSON.Net.Geometry
            try
            {
                var polygonPoints = JsonConvert.DeserializeObject<PolygonPoints>(feature);

                var coordinates = polygonPoints.geometry.coordinates[0];

                Coordinate[] array = new Coordinate[coordinates.Length];
                for (int i = 0; i < coordinates.Length; i++)
                {
                    array[i] = new Coordinate(coordinates[i][0], coordinates[i][1]);

                }

                Polygon polygon = new Polygon(new LinearRing(array));

                Coordinate point = new Coordinate(currentLat, currentLong);

                Point pointGeometry = new Point(point);

                return polygon.Contains(pointGeometry);
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
    }
}

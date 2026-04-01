
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
using MySqlConnector;
using Microsoft.Azure.ServiceBus;
using e4scoreDataIngestionFunctionApp.Models;
using e4scoreDataIngestionFunctionApp.Services;
using e4scoreDataIngestionFunctionApp.Interfaces;
using GoogleMapsApi.Entities.PlacesDetails.Response;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Reflection;
using Microsoft.Azure.Amqp.Framing;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace e4scoreDataIngestionFunctionApp.DataAccess
{
    public class MySQLDatabase : IMySQLDatabase
    {

        private readonly IDeviceProcessingQueue _deviceProcessingQueue;
        private readonly ezcheckinContext _dbContext;
        private readonly IAzureRedisCache _azureRedisCache;
        public MySQLDatabase(IDeviceProcessingQueue deviceProcessingQueue, ezcheckinContext db, IAzureRedisCache azureRedisCache)
        {
            _deviceProcessingQueue = deviceProcessingQueue;
            _dbContext = db;
            _azureRedisCache = azureRedisCache;
        }

        public DeviceEvent GetDeviceEventsByIMEI(MatrackRequest matrackRequest, ILogger log)
        {
            try
            {
                List<LastMoveDays> lastMoveDays = new List<LastMoveDays>();
                Models.DomainModels.Event events = new Models.DomainModels.Event();
                LastMovesInNDays lastMovesInNDays = new LastMovesInNDays();
                var latitude = Convert.ToString(matrackRequest.location.primary.latitude);
                var longitude = Convert.ToString(matrackRequest.location.primary.longitude);
                var findImei = _dbContext.EztrackDevices.Any(a => a.Imei == matrackRequest.imei && a.AssetId != null);
                if (!findImei)
                {
                    SaveUnIdentifiedDevices(matrackRequest);
                    return new DeviceEvent { DeviceNotFound = true };
                }

                var device = GetDeviceData(matrackRequest);

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

                //var eventExist = _dbContext.EztrackEvents.Any(a => a.Imei == matrackRequest.imei);
                //if (!eventExist)
                //{
                //    var device = GetDeviceData(matrackRequest);

                //    if (device != null)
                //    {
                //        events.imei = device.Imei;
                //        events.company_id = (long)device.OwnerId;
                //        events.date_of_last_move = matrackRequest.timestamp;
                //        events.assetid = device.AssetId;
                //        events.last_latitude = device.LastEventLatitude;
                //        events.last_longitude = device.LastEventLongitude;
                //        events.tracker_type = device.TrackerType;
                //        events.latitude = device.Latitude;
                //        events.longitude = device.Longitude;
                //        events.domicile_name1 = device.Name;
                //        events.asset_domicile_name = device.Name;
                //        events.asset_name1 = device.asset_name;
                //        events.asset_uuid = device.Uuid;
                //        events.moves_in_last3days = device.MovesInLast3days;
                //        events.moves_in_last7days = device.MovesInLast7days;
                //        events.moves_in_last30days = device.MovesInLast30days;
                //        events.moves_in_last60days = device.MovesInLast60days;
                //        events.moves_in_last90days = device.MovesInLast90days;
                //        events.zone = device.Zone;
                //        events.tracker_type = device.TrackerType;
                //    }
                //}
                //else
                //{
                //    var @event = GetEventsData(matrackRequest);
                //    if (events != null)
                //    {
                //        events.imei = @event.Imei;
                //        events.company_id = (long)@event.CompanyId;
                //        events.assetid = @event.assetid;
                //        events.last_latitude = @event.last_latitude;
                //        events.last_longitude = @event.last_longitude;
                //        events.tracker_type = @event.TrackerType;
                //        events.latitude = @event.Latitude;
                //        events.longitude = @event.Longitude;
                //        events.domicile_name1 = @event.Name;
                //        events.asset_domicile_name = @event.Name;
                //        events.asset_name1 = @event.asset_name;
                //        events.asset_uuid = @event.AssetUuid;
                //        events.moves_in_last3days = @event.MovesInLast3days;
                //        events.moves_in_last7days = @event.MovesInLast7days;
                //        events.moves_in_last30days = @event.MovesInLast30days;
                //        events.moves_in_last60days = @event.MovesInLast60days;
                //        events.moves_in_last90days = @event.MovesInLast90days;
                //        events.is_move = @event.IsMove;
                //        events.first_move_of_day = @event.FirstMoveOfDay;
                //        events.date_of_last_move = @event.DateOfLastMove;
                //        events.zone = @event.Zone;
                //        events.tracker_type = @event.TrackerType;
                //        events.ExcrusionTime = @event.ExcrusionTime;
                //        events.ExcrusionTimeStart = @event.ExcrusionTimeStart;
                //        events.DwellTime = @event.DwellTime;
                //        events.DwellTimeStart = @event.DwellTimeStart;
                //        events.source_timestamp = @event.SourceTimestamp;
                //    }

                if (events != null)
                {

                    lastMovesInNDays.three = events.moves_in_last3days is null ? 0 : (int)events.moves_in_last3days;
                    lastMovesInNDays.seven = events.moves_in_last7days is null ? 0 : (int)events.moves_in_last7days;
                    lastMovesInNDays.thirty = events.moves_in_last30days is null ? 0 : (int)events.moves_in_last30days;
                    lastMovesInNDays.sixty = events.moves_in_last60days is null ? 0 : (int)events.moves_in_last60days;
                    lastMovesInNDays.ninety = events.moves_in_last90days is null ? 0 : (int)events.moves_in_last90days;
                    lastMovesInNDays.source_timestamp = matrackRequest?.timestamp;
                }

                //    // var eventsDataNinty = _dbContext.EztrackEvents.Where(x => x.Imei == matrackRequest.imei && x.SourceTimestamp >= DateTime.Now.AddDays(-90) && x.FirstMoveOfDay == 1)
                //    //.OrderByDescending(x => x.SourceTimestamp).AsEnumerable()
                //    //.Select((x, index) => new
                //    //{
                //    //  source_timestamp = x.SourceTimestamp,
                //    //  serial_number = index + 1,
                //    //  imei = x.Imei,
                //    //  is_move = x.IsMove
                //    //}).ToList();  
                //    // foreach (var item in eventsDataNinty)
                //    // {
                //    //   LastMoveDays lastMoveDays1 = new LastMoveDays();
                //    //   lastMoveDays1.source_timestamp = item.source_timestamp.Value;
                //    //   lastMoveDays1.serial_number = item.serial_number;
                //    //   lastMoveDays1.is_move = item.is_move;
                //    //   lastMoveDays1.imei = item.imei;
                //    //   lastMoveDays.Add(lastMoveDays1);
                //    // }
                //}

                if (!events.imei.Any())
                {
                    // log.LogInformation($"[GetDeviceEvents] MYSQL Exception : {ex.Message} --------------------------------");
                    log.LogInformation($"Device with imei : {matrackRequest.imei} not found #######################");
                    return new DeviceEvent { EventNotFound = true };
                }
                return new DeviceEvent { Event = events, lastMovesInNDays = lastMovesInNDays }; //, LastMoveDays = lastMoveDays
            }
            catch (Exception ex)
            {
                log.LogInformation($"[GetDeviceEvents] MYSQL Exception : {ex.Message} --------------------------------");
                throw ex;
            }
        }

        public AddressInfo GetGeocodeLocation(MatrackRequest matrackRequest, ILogger log)
        {
            try
            {
                Models.GeocodeLocation geocodeLocation = new Models.GeocodeLocation();
                var watch = new Stopwatch();
                watch.Start();
                string latStr = matrackRequest.location.primary.latitude.ToString("0.000");
                string langStr = matrackRequest.location.primary.longitude.ToString("0.000");
                if (latStr.Length < 6 || langStr.Length < 7)
                {
                    return null;
                }
                geocodeLocation = _dbContext.GeocodeLocations.Where(a => a.Longitude == langStr && a.Latitude == latStr).FirstOrDefault();
                if (geocodeLocation is null)
                {
                    geocodeLocation = _dbContext.GeocodeLocations.Where(a => a.Longitude.Contains(langStr) && a.Latitude.Contains(latStr)).FirstOrDefault();
                }

                if (geocodeLocation == null)
                {
                    return null;
                }
                else
                {
                    var addressInfo = new AddressInfo
                    {
                        Address = geocodeLocation?.StreetAddress,
                        Postal = geocodeLocation?.Postal,
                        City = geocodeLocation?.Locality,
                        State = geocodeLocation?.State,
                        Country = geocodeLocation?.Country
                    };
                    watch.Stop();
                    log.LogWarning($"Get Geolocation from Database : {geocodeLocation?.StreetAddress} Time : {watch.Elapsed.Seconds} sec ");
                    return addressInfo;
                }
            }
            catch (Exception ex)
            {
                log.LogInformation($"[GetGeocodeLocation] MYSQL Exception : {ex.Message} --------------------------------");
                throw ex;
            }
        }

        public void SaveGeocodeLocation(MatrackRequest matrackRequest, AddressInfo addressInfo, ILogger log)
        {
            try
            {
                Models.GeocodeLocation geocodeLocation = new Models.GeocodeLocation();

                geocodeLocation.FullLocation = addressInfo.FullLocation;
                geocodeLocation.StreetAddress = addressInfo.Address == "" ? null : addressInfo.Address;
                geocodeLocation.Postal = addressInfo.Postal;
                geocodeLocation.State = addressInfo.State;
                geocodeLocation.Locality = addressInfo.City;
                geocodeLocation.Created = DateTime.UtcNow;
                geocodeLocation.Version = 0;
                geocodeLocation.Country = addressInfo.Country;
                geocodeLocation.Longitude = matrackRequest.location.primary.longitude.ToString("0.000");
                geocodeLocation.Latitude = matrackRequest.location.primary.latitude.ToString("0.000");
                var geocodeLocationExist = _dbContext.GeocodeLocations.Where(a => a.Longitude == geocodeLocation.Longitude && a.Latitude == geocodeLocation.Latitude).FirstOrDefault();
                if (geocodeLocationExist is null)
                {
                    _dbContext.GeocodeLocations.Add(geocodeLocation);
                    _dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                log.LogInformation($"[GetGeocodeLocation] MYSQL Exception : {ex.Message} --------------------------------");
                throw ex;
            }

        }


        private void SaveUnIdentifiedDevices(MatrackRequest matrackRequest)
        {

            var latitude = Convert.ToString(matrackRequest.location.primary.latitude);
            var longitude = Convert.ToString(matrackRequest.location.primary.longitude);

            UnidentifiedDeviceDatum unidentifiedDeviceDatum = new UnidentifiedDeviceDatum();
            unidentifiedDeviceDatum.Imei = matrackRequest.imei;
            unidentifiedDeviceDatum.Longitude = longitude;
            unidentifiedDeviceDatum.Latitude = latitude;
            unidentifiedDeviceDatum.SourceTimestamp = matrackRequest.timestamp;
            unidentifiedDeviceDatum.Created = DateTime.UtcNow;
            unidentifiedDeviceDatum.DeviceProvider = matrackRequest.location.located_with;
            _dbContext.UnidentifiedDeviceData.Add(unidentifiedDeviceDatum);
            _dbContext.SaveChanges();
        }


        private dynamic GetDeviceData(MatrackRequest matrackRequest)
        {
            var device = (from a in _dbContext.EztrackDevices.Where(a => a.Imei == matrackRequest.imei)
                          join b in _dbContext.Assets on a.AssetId equals b.Id
                          join l in _dbContext.Locations on b.DomicileId equals l.Id
                          select new
                          {
                              a.Imei,
                              a.OwnerId,
                              a.DateOfLastMove,
                              a.AssetId,
                              a.LastEventLatitude,
                              a.LastEventLongitude,
                              a.TrackerType,
                              l.Latitude,
                              l.Longitude,
                              l.Name,
                              asset_name = b.AssetId,
                              b.Uuid,
                              a.MovesInLast3days,
                              a.MovesInLast7days,
                              a.MovesInLast30days,
                              a.MovesInLast60days,
                              a.MovesInLast90days,
                              a.Zone,
                              SourceTimestamp = a.LatestEventDate,
                          }
                       ).FirstOrDefault();
            return device;
        }


        private dynamic GetEventsData(MatrackRequest matrackRequest)
        {
            var eventss = (from a in _dbContext.EztrackEvents.Where(a => a.Imei == matrackRequest.imei)
                           join p in _dbContext.PingLocations on a.LocationId equals p.Id
                           join b in _dbContext.Assets on a.AssetUuid equals b.Uuid
                           join l in _dbContext.Locations on b.DomicileId equals l.Id
                           select new
                           {
                               b.Uuid,
                               a.Id,
                               asset_name = b.AssetId,
                               assetid = b.Id,
                               b.MovesInLast3days,
                               b.MovesInLast7days,
                               b.MovesInLast30days,
                               b.MovesInLast60days,
                               b.MovesInLast90days,
                               l.Latitude,
                               l.Longitude,
                               l.Name,
                               l.CompanyId,
                               last_longitude = p.Longitude,
                               last_latitude = p.Latitude,
                               a.Imei,
                               a.TrackerType,
                               a.AssetUuid,
                               a.IsMove,
                               a.FirstMoveOfDay,
                               a.DateOfLastMove,
                               a.Zone,
                               a.ExcrusionTime,
                               a.ExcrusionTimeStart,
                               a.DwellTime,
                               a.DwellTimeStart,
                               a.SourceTimestamp

                           }
                               ).OrderByDescending(a => a.Id).FirstOrDefault();
            return eventss;
        }


    }

}

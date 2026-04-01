using System;
using e4scoreDataIngestionFunctionApp.Models;
using GoogleMapsApi.Entities.Directions.Response;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using MySqlConnector;
using e4scoreDataIngestionFunctionApp.Models.Enum;
using System.Data;
using e4scoreDataIngestionFunctionApp.Models.RequestModels;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data.Common;
using System.Xml;
using e4scoreDataIngestionFunctionApp.Models.DomainModels;
using e4scoreDataIngestionFunctionApp.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;
using System.Transactions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using e4scoreDataIngestionFunctionApp.Services;

namespace e4scoreDataIngestionFunctionApp
{
    public class DeviceProcessingQueueTrigger
    {
        private readonly ezcheckinContext _dbContext;
        private readonly IE4EAIQueue _e4EAIQueue;
        public DeviceProcessingQueueTrigger(ezcheckinContext db, IE4EAIQueue e4EAIQueue)
        {
            _dbContext = db;
            _e4EAIQueue = e4EAIQueue;
        }
        [FunctionName("DeviceProcessingQueueTrigger")]
        public async Task Run([ServiceBusTrigger("deviceprocessing", Connection = ApplicationSettings.MaTrackQueueConnection)] string myQueueItem,
            ILogger log)
        { 
            var deviceProcessing = JsonConvert.DeserializeObject<DeviceProcessing>(myQueueItem);
            List<object> list = new List<object>();
            Models.PingSensor pingSensor = new Models.PingSensor();
            Models.PingLocation pingLocation = new Models.PingLocation();
            EztrackEvent eztrackEvent = new EztrackEvent();
            Asset eztrackAsset = new Asset();
            DateTime? dateOfLastMoveUpdated = null;
            deviceProcessing.MessageId = Guid.NewGuid().ToString();


            using (var scope = _dbContext.Database.BeginTransaction())
            {
                var watch = new System.Diagnostics.Stopwatch();
                watch.Start();
                try
                {
                    log.LogWarning($"Device Processing Function App Started IMEI : {deviceProcessing.IMEI} -----------------------------------------------------");


                    var eztrackDevice = _dbContext.EztrackDevices.Where(a => a.Imei == deviceProcessing.IMEI).FirstOrDefault();
                    if (eztrackDevice != null)
                    {

                        if (deviceProcessing.IsMove == false)
                        {
                            var eventById = _dbContext.EztrackEvents.OrderBy(a => a.Id).LastOrDefault(a => a.Imei == deviceProcessing.IMEI);
                            if (eventById is not null)
                            {
                                dateOfLastMoveUpdated = eventById.DateOfLastMove;
                            }
                        }
                        else
                        {
                            dateOfLastMoveUpdated = deviceProcessing.SourceTimestamp;
                        }



                        pingSensor.Created = DateTime.UtcNow;
                        pingSensor.Deleted = 0;
                        pingSensor.Enabled = 1;
                        pingSensor.Uuid = Guid.NewGuid().ToString();
                        pingSensor.Version = 1;
                        pingSensor.Battery = (float)deviceProcessing?.Battery;
                        pingSensor.Temperature = deviceProcessing.Temperature == 0 ? null : deviceProcessing.Temperature;
                        _dbContext.PingSensors.Add(pingSensor);

                        pingLocation.Created = DateTime.UtcNow;
                        pingLocation.Deleted = 0;
                        pingLocation.Enabled = 1;
                        pingLocation.Uuid = Guid.NewGuid().ToString();
                        pingLocation.Version = 1;
                        pingLocation.Latitude = deviceProcessing.Latitude;
                        pingLocation.Longitude = deviceProcessing.Longitude;
                        _dbContext.PingLocations.Add(pingLocation);
                        _dbContext.SaveChanges();


                        var assetForAssetName = _dbContext.Assets.Where(a => a.Uuid == deviceProcessing.AssetUuid).FirstOrDefault();

                        eztrackEvent.DateOfLastMove = dateOfLastMoveUpdated;
                        eztrackEvent.Created = DateTime.UtcNow;
                        eztrackEvent.Deleted = 0;
                        eztrackEvent.Enabled = 1;
                        eztrackEvent.Uuid = Guid.NewGuid().ToString();
                        eztrackEvent.Version = 1;
                        eztrackEvent.Address = deviceProcessing.Address;
                        eztrackEvent.AssetUuid = deviceProcessing.AssetUuid;
                        eztrackEvent.City = deviceProcessing.City;
                        eztrackEvent.Direction = deviceProcessing.Direction;
                        eztrackEvent.DistanceFromDomicileInMeters = deviceProcessing.DistanceFromDomicile;
                        eztrackEvent.DistanceFromPreviousEventInMeters = deviceProcessing.DistanceFromPreviousEvent;
                        //eztrackEvent.EventType = deviceProcessing.EventType;  
                        eztrackEvent.FirstMoveOfDay = deviceProcessing.FirstMoveOfDay;
                        eztrackEvent.Fuel = deviceProcessing.Fuel;
                        eztrackEvent.GeofenceAddress = deviceProcessing.GeofenceAddress;
                        eztrackEvent.GeofenceCity = deviceProcessing.GeofenceCity;
                        eztrackEvent.GeofencePostal = deviceProcessing.GeofencePostal;
                        eztrackEvent.GeofenceState = deviceProcessing.GeofenceState;
                        eztrackEvent.GeofenceState = deviceProcessing.GeofenceState;
                        eztrackEvent.IdleTime = deviceProcessing.IdleTime;
                        eztrackEvent.Imei = deviceProcessing.IMEI;
                        eztrackEvent.IsMove = (ulong)(deviceProcessing.IsMove == true ? 1 : 0);
                        eztrackEvent.LocatedWith = null;
                        eztrackEvent.LocationName = deviceProcessing.LocationName;
                        eztrackEvent.Mileage = 0;
                        eztrackEvent.MoveThresholdInMeters = 804.67;
                        eztrackEvent.MovesInLast30days = deviceProcessing.Thirty;
                        eztrackEvent.PingType = null;
                        eztrackEvent.Postal = deviceProcessing.Postal;
                        eztrackEvent.Sequence = 0;
                        eztrackEvent.SourceCreatedAt = null;
                        eztrackEvent.SourceTimestamp = deviceProcessing.SourceTimestamp;
                        eztrackEvent.SourceUuid = null;
                        eztrackEvent.Speed = deviceProcessing.Speed;
                        eztrackEvent.State = deviceProcessing.State;
                        eztrackEvent.GroupId = null;
                        eztrackEvent.LocationId = pingLocation.Id;
                        eztrackEvent.SensorsId = pingSensor.Id;
                        eztrackEvent.Zone = deviceProcessing.zone;
                        eztrackEvent.PingEventUuid = null;
                        eztrackEvent.AssetDomicileName = deviceProcessing.AssetDomicile;
                        eztrackEvent.AssetName = assetForAssetName.AssetId;
                        eztrackEvent.TrackerType = deviceProcessing.TrackerType == null ? "WiFiCellular" : deviceProcessing.TrackerType;
                        eztrackEvent.DwellTimeStart = deviceProcessing.DwellTime.DwellTimeStart;
                        eztrackEvent.DwellTime = deviceProcessing.DwellTime.Days;
                        eztrackEvent.ExcrusionTimeStart = deviceProcessing.ExcursionTime.ExcursionTimeStart;
                        eztrackEvent.ExcrusionTime = deviceProcessing.ExcursionTime.Days;
                        eztrackEvent.Updated = DateTime.UtcNow;
                        _dbContext.EztrackEvents.Add(eztrackEvent);
                        _dbContext.SaveChanges();

                        var startDateEvent = _dbContext.EztrackEvents.Where(a => a.Imei == deviceProcessing.IMEI).FirstOrDefault().Created;
                        var endDateEvent = _dbContext.EztrackEvents.Where(a => a.Imei == deviceProcessing.IMEI).OrderBy(a => a.Id).LastOrDefault().Created;

                        eztrackDevice.Updated = DateTime.UtcNow;
                        eztrackDevice.Version = eztrackDevice.Version + 1;
                        eztrackDevice.TrackerType = deviceProcessing.TrackerType;
                        eztrackDevice.OwnerId = deviceProcessing.CompanyId;
                        eztrackDevice.Battery = deviceProcessing.Battery;
                        eztrackDevice.LastEventLatitude = deviceProcessing.Latitude;
                        eztrackDevice.LastEventLongitude = deviceProcessing.Longitude;
                        eztrackDevice.DistanceFromDomicileInMeters = deviceProcessing.DistanceFromDomicile;
                        eztrackDevice.AssetId = deviceProcessing.AssetId;
                        eztrackDevice.LatestEventAddress = string.IsNullOrEmpty(deviceProcessing.GeofenceAddress) ? deviceProcessing.Address : deviceProcessing.GeofenceAddress;
                        eztrackDevice.LatestEventCity = string.IsNullOrEmpty(deviceProcessing.GeofenceCity) ? deviceProcessing.City : deviceProcessing.GeofenceCity;
                        eztrackDevice.LatestEventState = string.IsNullOrEmpty(deviceProcessing.GeofenceState) ? deviceProcessing.State : deviceProcessing.GeofenceState;
                        eztrackDevice.LatestEventPostal = string.IsNullOrEmpty(deviceProcessing.GeofencePostal) ? deviceProcessing.Postal : deviceProcessing.GeofencePostal;
                        eztrackDevice.DaysOfEventHistory = (endDateEvent - startDateEvent).Value.Days;
                        eztrackDevice.LatestEventDate = deviceProcessing.SourceTimestamp;
                        eztrackDevice.LocationName = deviceProcessing.LocationName;
                        eztrackDevice.DwellTimeStart = deviceProcessing?.DwellTime?.DwellTimeStart;
                        eztrackDevice.ExcrusionTimeStart = deviceProcessing?.ExcursionTime?.ExcursionTimeStart;
                        eztrackDevice.DateOfLastMove = dateOfLastMoveUpdated;
                        _dbContext.Entry(eztrackDevice).State = EntityState.Modified;

                        eztrackAsset = _dbContext.Assets.Where(a => a.Uuid == deviceProcessing.AssetUuid).FirstOrDefault();
                        eztrackAsset.Updated = DateTime.UtcNow;
                        eztrackAsset.Version = eztrackDevice.Version + 1;
                        eztrackAsset.CompanyId = deviceProcessing.CompanyId;
                        eztrackAsset.Battery = deviceProcessing.Battery;
                        eztrackAsset.LastEventLatitude = deviceProcessing.Latitude;
                        eztrackAsset.LastEventLongitude = deviceProcessing.Longitude;
                        eztrackAsset.DistanceFromDomicileInMeters = deviceProcessing.DistanceFromDomicile;
                        eztrackAsset.LatestEventAddress = string.IsNullOrEmpty(deviceProcessing.GeofenceAddress) ? deviceProcessing.Address : deviceProcessing.GeofenceAddress;
                        eztrackAsset.LatestEventCity = string.IsNullOrEmpty(deviceProcessing.GeofenceCity) ? deviceProcessing.City : deviceProcessing.GeofenceCity;
                        eztrackAsset.LatestEventState = string.IsNullOrEmpty(deviceProcessing.GeofenceState) ? deviceProcessing.State : deviceProcessing.GeofenceState;
                        eztrackAsset.LatestEventPostal = string.IsNullOrEmpty(deviceProcessing.GeofencePostal) ? deviceProcessing.Postal : deviceProcessing.GeofencePostal;
                        eztrackAsset.DaysOfEventHistory = (endDateEvent - startDateEvent).Value.Days;
                        eztrackAsset.LatestEventDate = deviceProcessing.SourceTimestamp;
                        eztrackAsset.MovesInLast3days = deviceProcessing.Three;
                        eztrackAsset.MovesInLast7days = deviceProcessing.Seven;
                        eztrackAsset.MovesInLast30days = deviceProcessing.Thirty;
                        eztrackAsset.MovesInLast60days = deviceProcessing.Sixty;
                        eztrackAsset.MovesInLast90days = deviceProcessing.Ninety;
                        eztrackAsset.TemperatureInc = deviceProcessing.Temperature == 0 ? null : deviceProcessing.Temperature;
                        eztrackAsset.LocationName = deviceProcessing.LocationName;
                        eztrackAsset.DateOfLastMove = dateOfLastMoveUpdated;
                        _dbContext.Entry(eztrackAsset).State = EntityState.Modified;

                        EztrackDeviceEvent eztrackDeviceEvents = new EztrackDeviceEvent();
                        eztrackDeviceEvents.EztrackDeviceId = eztrackDevice.Id;
                        eztrackDeviceEvents.EventsId = eztrackEvent.Id;
                        _dbContext.EztrackDeviceEvents.Add(eztrackDeviceEvents);

                        _dbContext.SaveChanges();
                        scope.Commit();

                        //var location = _dbContext.Locations.Where(l => l.Id == deviceProcessing.LocationId).FirstOrDefault();
                        //var equipment = _dbContext.Equipment.Where(e => e.Id == eztrackAsset.EquipmentId).FirstOrDefault();
                        //var carrer = _dbContext.Carriers.Where(c => c.Id == eztrackAsset.CarrierId).FirstOrDefault();

                        //deviceProcessing.PartnerName = carrer?.Name != null ? carrer.Name : null;
                        //deviceProcessing.DeviceId = eztrackDevice?.Imei != null ? eztrackDevice.Imei : null;
                        //deviceProcessing.PartnerId = eztrackAsset?.CarrierId != null ? Convert.ToInt32(eztrackAsset.CarrierId) : null;
                        //deviceProcessing.EquipmentType = equipment?.Name != null ? equipment.Name : null;
                        //deviceProcessing.VIN = eztrackAsset?.AssetVinNumber != null ? eztrackAsset.AssetVinNumber : null;
                        //deviceProcessing.LocationCode = location?.Code != null ? location.Code : null;
                        //deviceProcessing.Country = location?.Country != null ? location.Country : null;
                        //deviceProcessing.TimeZone = location?.Timezone != null ? location.Timezone : null;
                        //deviceProcessing.LastReportedTime = deviceProcessing?.SourceTimestamp;
                        //deviceProcessing.DateOfLastMove = eztrackAsset?.DateOfLastMove != null ? eztrackAsset.DateOfLastMove : null;
                        //deviceProcessing.DistanceFromDomicileInMeters = eztrackAsset?.DistanceFromDomicileInMeters != null ? Convert.ToDecimal(eztrackAsset.DistanceFromDomicileInMeters) : null;
                        //deviceProcessing.MoveFrequency = null;
                        //deviceProcessing.ShipmentNumber = eztrackAsset?.LatestShipmentId != null ? eztrackAsset.LatestShipmentId.ToString() : null;

                        ////Send consolidated model of DeviceProcessing to the e4-eai-queue for save in the database
                        //await _e4EAIQueue.SendAsync(deviceProcessing, log);



                    }
                    else
                    {
                        log.LogWarning($"Device with imei : {deviceProcessing.IMEI} not found #######################");
                    }



                }
                catch (Exception ex)
                {
                    log.LogError($"[Device Processing] MYSQL Exception : {ex.Message}  --------------------------------");
                    scope.Rollback();
                    throw new Exception(ex.Message);
                }
                finally
                {
                    log.LogWarning($"Function Execution Time (success): {watch.ElapsedMilliseconds} ms {watch.Elapsed.Seconds} sec ------------------------------------------------------");
                    log.LogWarning("Device processing ended ------------------------------------------------------");
                    log.LogWarning($" ");
                }
            }
        }
    }
}

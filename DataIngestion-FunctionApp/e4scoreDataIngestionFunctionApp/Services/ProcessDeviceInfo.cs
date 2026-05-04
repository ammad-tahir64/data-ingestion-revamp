using e4scoreDataIngestionFunctionApp.Models.RequestModels;
using e4scoreDataIngestionFunctionApp.DataAccess;
using e4scoreDataIngestionFunctionApp.Interfaces;
using e4scoreDataIngestionFunctionApp.Models.DomainModels;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using GoogleMapsApi.Entities.PlacesDetails.Response;
using e4scoreDataIngestionFunctionApp.Helpers.Extensions;
using System;
using System.Linq;
using System.Collections.Generic;
using e4scoreDataIngestionFunctionApp.Helpers;
using e4scoreDataIngestionFunctionApp.Models.GeoLocation;
using System.Globalization;
using e4scoreDataIngestionFunctionApp.Models;
using GoogleMapsApi.Entities.Geocoding.Response;
using GoogleMapsApi.Entities.Directions.Response;
using System.Diagnostics;
using e4scoreDataIngestionFunctionApp.Models.Enum;
using e4scoreDataIngestionFunctionApp.Helpers.Mappers;

namespace e4scoreDataIngestionFunctionApp.Services
{
    public class ProcessDeviceInfo : IProcessDeviceInfo
    {
        private readonly IMySQLDatabase _mySQLDatabase;
        private readonly IAzureRedisCache _azureRedisCache;
        private readonly IReverseGeoCoding _reverseGeoCoding;
        private readonly ICalculateDwellTime _calculateDwellTime;
        private readonly ICalculateExcursionTime _calculateExcursionTime;
        private readonly IBusinessEnrichmentQueueSender _businessEnrichmentQueue;

        public ProcessDeviceInfo(
            IMySQLDatabase mySQLDatabase,
            IAzureRedisCache azureRedisCache,
            IReverseGeoCoding reverseGeoCoding,
            ICalculateDwellTime calculateDwellTime,
            ICalculateExcursionTime calculateExcursionTime,
            IBusinessEnrichmentQueueSender businessEnrichmentQueue)
        {
            _mySQLDatabase = mySQLDatabase;
            _azureRedisCache = azureRedisCache;
            _reverseGeoCoding = reverseGeoCoding;
            _calculateDwellTime = calculateDwellTime;
            _calculateExcursionTime = calculateExcursionTime;
            _businessEnrichmentQueue = businessEnrichmentQueue;
        }

        /// <summary>
        /// Process the data and calculate the fields.
        /// </summary>
        /// <param name="matrackRequest"></param>
        /// <param name="log"></param>
        /// <returns>true if the process is successful </returns>
        public async Task<bool> Process(MatrackRequest matrackRequest, ILogger log)
        {
            try
            {
                LastMovesInNDays movesInNDays = new LastMovesInNDays();

                MatrackRequest lastLatLong = _azureRedisCache.GetDeviceRuntimeCache(matrackRequest.imei);

                var cachedtLastMoves = _azureRedisCache.GetMovesInLastNDaysCache(matrackRequest.imei);

                var cachedEvent = _azureRedisCache.GetEventCache(matrackRequest.imei);



                int is_move = 0;
                long idelTime = 0;

                //Update caches from database if :
                //there is no device entry in database of that particular device
                //there is no event of that device
                //device and event exists in the database but cache doesn't exists
                log.LogWarning($"+++++++++++++++++++++++++++++++++++ From Cache +++++++++++++++++++++++++++++++++++");
                if (cachedEvent == null || (cachedtLastMoves == null))
                {
                    log.LogWarning($"+++++++++++++++++++++++++++++++++++ From Database everytime +++++++++++++++++++++++++++++++++++");
                    var DeviceEvent = _mySQLDatabase.GetDeviceEventsByIMEI(matrackRequest, log);
                    if (DeviceEvent.DeviceNotFound)
                    {
                        log.LogWarning($"Device with imei : {matrackRequest.imei} not found #######################");
                        _azureRedisCache.SetUnidentifiedCache(matrackRequest.imei);
                        return true;
                    }
                    _azureRedisCache.SetEventCache(DeviceEvent.Event);
                    cachedEvent = _azureRedisCache.GetEventCache(matrackRequest.imei);
                    _azureRedisCache.SetMovesInLastNDaysCache(DeviceEvent.lastMovesInNDays, matrackRequest.imei);
                }
                _azureRedisCache.SetEventCache(cachedEvent);

                _azureRedisCache.SetDeviceRuntimeCache(matrackRequest);

                DateTime currentJSONdate = Convert.ToDateTime(matrackRequest.timestamp).Date;
                DateTime? previousJSONdate = DateTime.MinValue;
                int first_move_of_day = 0;
                double distanceFromPreviousEvent = 0;
                bool isMove = false;


                // Populates fields on the basis if there is previous activity of the asset
                if (lastLatLong != null)
                {
                    previousJSONdate = Convert.ToDateTime(lastLatLong.timestamp).Date == null ? currentJSONdate : Convert.ToDateTime(lastLatLong.timestamp).Date;
                    isMove = lastLatLong.location.primary.latitude != matrackRequest.location.primary.latitude && lastLatLong.location.primary.longitude != matrackRequest.location.primary.longitude;
                    distanceFromPreviousEvent = GeoDistance.Distance(Convert.ToDouble(lastLatLong.location.primary.latitude), Convert.ToDouble(lastLatLong.location.primary.longitude), matrackRequest.location.primary.latitude, matrackRequest.location.primary.longitude);
                    first_move_of_day = currentJSONdate != previousJSONdate && isMove && distanceFromPreviousEvent >= Constants.HalfMileInMeters ? 1 : 0;
                }

                // Asset move must be more than 0.5 miles
                if (isMove && distanceFromPreviousEvent >= Constants.HalfMileInMeters)
                {
                    //update cache i.e remove first and add into last
                    //set updated cache
                    var getLastMoves = _azureRedisCache.GetMovesInLastNDaysCache(matrackRequest.imei);
                    if (getLastMoves != null)
                    {
                        movesInNDays = PopulateMovesInNDays(getLastMoves, matrackRequest);
                        _azureRedisCache.SetMovesInLastNDaysCache(movesInNDays, matrackRequest.imei);
                    }
                    is_move = 1;
                    idelTime = 0;
                    _azureRedisCache.SetDateOfLastMoveCache(matrackRequest.timestamp, matrackRequest.imei);

                }
                else
                {
                    var getLastMoves = _azureRedisCache.GetMovesInLastNDaysCache(matrackRequest.imei);
                    if (getLastMoves != null)
                    {
                        movesInNDays = PopulateMovesInNDays(getLastMoves, matrackRequest);
                        _azureRedisCache.SetMovesInLastNDaysCache(movesInNDays, matrackRequest.imei);

                    }
                    distanceFromPreviousEvent = 0;
                    is_move = 0;
                    string dateOfLastMove = _azureRedisCache.GetDateOfLastMoveCache(matrackRequest.imei);


                    //Calculate idle time
                    //update date of last move cache
                    //if we dont find date of last move in event table, update cache from device table
                    if (dateOfLastMove is null && cachedEvent != null && cachedEvent.date_of_last_move is not null && cachedEvent.is_move == 0)
                    {
                        _azureRedisCache.SetDateOfLastMoveCache(cachedEvent.date_of_last_move.Value, matrackRequest.imei);

                        DateTime previousTimeStamp = DatetimeHelper.ParseDatetime(cachedEvent.date_of_last_move.ToString());
                        var days = (matrackRequest.timestamp - previousTimeStamp).TotalDays;
                        idelTime = previousTimeStamp == DateTime.MinValue ? 0 : TimeSpanExtension.FromDaysToNanoseconds(days);
                    }
                    else if (dateOfLastMove != null)
                    {
                        DateTime previousTimeStamp = DatetimeHelper.ParseDatetime(dateOfLastMove);
                        var days = (matrackRequest.timestamp - previousTimeStamp).TotalDays;
                        idelTime = previousTimeStamp == DateTime.MinValue ? 0 : TimeSpanExtension.FromDaysToNanoseconds(days);
                    }

                    else
                    {

                        idelTime = 0;
                    }
                    if (idelTime < 0)
                    {
                        log.LogWarning($"DOLM: {dateOfLastMove} , Request: {matrackRequest.timestamp}");
                        if (!string.IsNullOrEmpty(dateOfLastMove))
                        {
                            DateTime previousTimeStamp = ParseDatetimeCorrect(dateOfLastMove);
                            var days = (matrackRequest.timestamp - previousTimeStamp).TotalDays;
                            idelTime = previousTimeStamp == DateTime.MinValue ? 0 : TimeSpanExtension.FromDaysToNanoseconds(days);
                        }
                    }
                    if (idelTime < 0)
                    {
                        idelTime = 0;
                    }
                }

                log.LogWarning($"Idle Time: {idelTime} ");
                ///Firstly the function looks i
                RedisLocation locationDetails = new RedisLocation();

                if (cachedEvent is not null)
                {
                    locationDetails = _calculateDwellTime.LocationInfoByLatLong(matrackRequest.location.primary.longitude, matrackRequest.location.primary.latitude, cachedEvent.company_id);

                }
                else
                {
                    locationDetails = null;
                }

                //Device is not within a geofence then look into database first and then Google
                var addressInfo = await PopulateAddressInfo(matrackRequest, cachedEvent, locationDetails, log);

                //Calculate dwell time
                var dwellTime = CalculateDwellTime(locationDetails, matrackRequest.timestamp, matrackRequest.imei, cachedEvent);
                log.LogWarning($"Dwell Time Days: {dwellTime.Days} ");


                //Calculate excusrion time
                var excursionTime = CalculateExcursionTime(locationDetails, matrackRequest.timestamp, matrackRequest.imei, cachedEvent);
                log.LogWarning($"Excursion Time Days: {excursionTime.Days} ");



                log.LogWarning($"Mapping to device processing");
                var deviceProcessing = Mappers.DeviceInfoToDeviceProcessing(matrackRequest, cachedEvent, addressInfo, first_move_of_day, is_move, distanceFromPreviousEvent, idelTime, movesInNDays, dwellTime, locationDetails, excursionTime);

                //Update cache for asset profile
                _azureRedisCache.SetAssetProfileCache(matrackRequest.imei, deviceProcessing.DwellTime, deviceProcessing.ExcursionTime, idelTime);

                //Send consolidated model of DeviceProcessing to the device processing queue for save in the database
                return await _businessEnrichmentQueue.SendAsync(deviceProcessing, log);
            }
            catch (Exception ex)
            {
                var st = new StackTrace(ex, true);
                // Get the top stack frame
                var frame = st.GetFrame(0);
                // Get the line number from the stack frame
                var line = frame.GetFileLineNumber();
                log.LogError($"Exception on IMEI: {matrackRequest.imei} Exception {ex.Message} on line no : {line} --------------------------------");
                throw;
            }
        }

        private DwellResponse CalculateDwellTime(RedisLocation locationDetails, DateTime sourceTimeStamp, string imei, Models.DomainModels.Event deviceEvent)
        {
            var dwellTime = _calculateDwellTime.GetDwellTime(locationDetails, sourceTimeStamp, imei, deviceEvent);

            //Update total dwell time in days when there is stop time 
            //else set it to 0, running dwell time will be calculated from cache directly since we cannot change java api
            if (dwellTime.DwellTimeStart == null && dwellTime.DwellTimeStop != null)
            {
                dwellTime.Days = TimeSpanExtension.FromDaysToNanoseconds(dwellTime.Days);
            }
            else
            {
                dwellTime.Days = 0;
            }
            return dwellTime;
        }

        private ExcusrionResponse CalculateExcursionTime(RedisLocation locationDetails, DateTime sourceTimeStamp, string imei, Models.DomainModels.Event deviceEvent)
        {
            var excursionTime = _calculateExcursionTime.GetExcursionTime(locationDetails, sourceTimeStamp, imei, deviceEvent);

            //Update total excusrion time in days when there is stop time 
            //else set it to 0, running excusrion time will be calculated from cache directly since we cannot change java api
            if (excursionTime.ExcusrionTimeStart == null && excursionTime.ExcusrionTimeStop != null)
            {
                excursionTime.Days = TimeSpanExtension.FromDaysToNanoseconds(excursionTime.Days);

            }
            else
            {
                excursionTime.Days = 0;
            }
            return excursionTime;
        }

        private async Task<AddressInfo> PopulateAddressInfo(MatrackRequest matrackRequest, Models.DomainModels.Event cachedEvent, RedisLocation locationDetails, ILogger log)
        {
            AddressInfo addressInfo = new AddressInfo();

            if (locationDetails is null)
            {
                addressInfo = _mySQLDatabase.GetGeocodeLocation(matrackRequest, log);

                if (addressInfo == null || addressInfo.Address is null)
                {
                    addressInfo = await _reverseGeoCoding.GetAddressInfoFromGoogleApi(matrackRequest, log);

                    log.LogWarning($"Get Geolocation from Google : {addressInfo?.Address} ");
                }
            }

            return addressInfo;
        }

        /// <summary>
        /// Calculates last moves in N of days
        /// </summary>
        /// <param name="movesInNDays"></param>
        /// <param name="matrackRequest"></param>
        /// <returns>last moves in N of days</returns>
        private LastMovesInNDays PopulateMovesInNDays(LastMovesInNDays movesInNDays, MatrackRequest matrackRequest)
        {
            LastMovesInNDays movesInNDays2 = new LastMovesInNDays();
            if (movesInNDays.source_timestamp is not null)
            {
                var dateDiff = matrackRequest.timestamp.AddDays(-1).Day - matrackRequest.timestamp.Day;
                if (dateDiff < 1)
                {
                    return movesInNDays;
                }
                if (dateDiff <= 3)
                {
                    movesInNDays2.three = (movesInNDays.three + 1);
                    movesInNDays2.seven = (movesInNDays.seven + 1);
                    movesInNDays2.thirty = (movesInNDays.thirty + 1);
                    movesInNDays2.sixty = (movesInNDays.sixty + 1);
                    movesInNDays2.ninety = (movesInNDays.ninety + 1);
                    movesInNDays2.source_timestamp = matrackRequest.timestamp;
                }
                else if (dateDiff <= 7)
                {
                    movesInNDays2.seven = (movesInNDays.seven + 1);
                    movesInNDays2.thirty = (int)(movesInNDays.thirty + 1);
                    movesInNDays2.sixty = (int)(movesInNDays.sixty + 1);
                    movesInNDays2.ninety = (int)(movesInNDays.ninety + 1);
                    movesInNDays2.source_timestamp = matrackRequest.timestamp;

                }
                else if (dateDiff <= 30)
                {
                    movesInNDays2.thirty = (int)(movesInNDays.thirty + 1);
                    movesInNDays2.sixty = (int)(movesInNDays.sixty + 1);
                    movesInNDays2.ninety = (int)(movesInNDays.ninety + 1);
                    movesInNDays2.source_timestamp = matrackRequest.timestamp;

                }
                else if (dateDiff <= 60)
                {
                    movesInNDays2.sixty = (int)(movesInNDays.sixty + 1);
                    movesInNDays2.ninety = (int)(movesInNDays.ninety + 1);
                    movesInNDays2.source_timestamp = matrackRequest.timestamp;

                }
                else if (dateDiff <= 90)
                {
                    movesInNDays2.ninety = (int)(movesInNDays.ninety + 1);
                    movesInNDays2.source_timestamp = matrackRequest.timestamp;

                }
            }
            return movesInNDays;
        }


        public DateTime ParseDatetimeCorrect(string dateTime)
        {
            //string inputDate = "15/02/2023 4:30:00 PM"; //example input date string
            DateTime parsedDate;

            if (
               DateTime.TryParseExact(dateTime, "MM/dd/yyyy h:mm:ss tt", null, System.Globalization.DateTimeStyles.None, out parsedDate)

             || DateTime.TryParseExact(dateTime, "M/dd/yyyy h:mm:ss tt", null, System.Globalization.DateTimeStyles.None, out parsedDate)

             || DateTime.TryParseExact(dateTime, "M/d/yyyy h:mm:ss tt", null, System.Globalization.DateTimeStyles.None, out parsedDate)
             || DateTime.TryParseExact(dateTime, "dd/MM/yyyy h:mm:ss tt", null, System.Globalization.DateTimeStyles.None, out parsedDate)
             || DateTime.TryParseExact(dateTime, "d/MM/yyyy h:mm:ss tt", null, System.Globalization.DateTimeStyles.None, out parsedDate)
             )
            {
                return parsedDate;
            }
            else
            {
                return DateTime.MinValue;
            }

        }


    }
}

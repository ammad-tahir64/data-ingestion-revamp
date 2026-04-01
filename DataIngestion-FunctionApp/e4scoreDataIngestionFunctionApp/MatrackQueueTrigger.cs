using e4scoreDataIngestionFunctionApp.Models.RequestModels;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using e4scoreDataIngestionFunctionApp.Interfaces;
using System;
using System.Threading.Tasks;
using e4scoreDataIngestionFunctionApp.Models.Enum;
using System.Globalization;
using GoogleMapsApi.Entities.Directions.Response;
using Microsoft.WindowsAzure.Storage.File.Protocol;
using System.Collections.Generic;
using e4scoreDataIngestionFunctionApp.Models;
using e4scoreDataIngestionFunctionApp.Services;
using Location = e4scoreDataIngestionFunctionApp.Models.RequestModels.Location;

namespace e4scoreDataIngestionFunctionApp
{
    public class MatrackQueueTrigger
    {
        private readonly IProcessDeviceInfo _processDeviceInfo;

        private readonly IMessageSegmentationQueue _segmentationQueue;
        private readonly IRebuildQueue _matrack;
        public MatrackQueueTrigger(IProcessDeviceInfo processDeviceInfo, IMessageSegmentationQueue segmentationQueue, IRebuildQueue matrack)
        {
            _processDeviceInfo = processDeviceInfo;
            _segmentationQueue = segmentationQueue;
            _matrack = matrack;
        }

        [FunctionName(ApplicationSettings.MatrackFunctionName)]
        public async System.Threading.Tasks.Task Run([ServiceBusTrigger(ApplicationSettings.MatrackQueueName, Connection = ApplicationSettings.MaTrackQueueConnection)] string myQueueItem, ILogger log)
        {
            try
            {
                log.LogInformation($"Packet : {myQueueItem} -----------------------------------------------------");
                var watch = new System.Diagnostics.Stopwatch();
                watch.Start();

                var matrackRequests = this.FilterRequests(myQueueItem);

                log.LogWarning($"[Prod] Matrack Function App Start IMEI : {matrackRequests[0].imei} -----------------------------------------------------");


                int count = 1;
                foreach (var matrackRequest in matrackRequests)
                {
                    log.LogWarning($"[Prod] Executing message no: {count} IMEI: {matrackRequest.imei}-----------------------------------------------------");
                    if (matrackRequest.timestamp.Year >= DateTime.Now.Year)
                    {
                        if(matrackRequest.location is not null && matrackRequest.location.located_with is not null)
                        {
                            if (matrackRequest.location.located_with.ToLower().Contains("samsara") || matrackRequest.location.located_with.ToLower().Contains("roadready"))
                            {
                                await _matrack.SendAsync(matrackRequest, log);
                            }
                        } 
                            await _segmentationQueue.SendAsync(matrackRequest, log);
                        
                        //  var result = await _processDeviceInfo.Process(matrackRequest, log);
                    }
                    count++;
                }

                watch.Stop();
                log.LogWarning($"Function Execution Time (success): {watch.ElapsedMilliseconds} ms {watch.Elapsed.Seconds} sec ------------------------------------------------------");
                log.LogWarning($" ");

            }
            catch (Exception ex)
            {
                    throw ex;

            }
        }

        private List<MatrackRequest> FilterRequests(string myQueueItem)
        {
            var matrackRequests = new List<MatrackRequest>();
            if (myQueueItem.Contains("data"))
            {
                matrackRequests = FilterBatchRequest(myQueueItem);
            }
            else
            {
                matrackRequests = FilterSingleRequest(myQueueItem);
            }

            return matrackRequests;
        }
        private List<MatrackRequest> FilterSingleRequest(string myQueueItem)
        {
            var matrackRequests = new List<MatrackRequest>();
            var matrackRequest = new MatrackRequest();
            if (myQueueItem.Contains("samsara"))
            {
                var samsaraRequest = JsonConvert.DeserializeObject<Samsara>(myQueueItem);

                matrackRequest = new MatrackRequest
                {
                    timestamp = samsaraRequest.timestamp,
                    location = new Location
                    {
                        primary = new Primary
                        {
                            latitude = samsaraRequest.location.primary.latitude,
                            longitude = samsaraRequest.location.primary.longitude,
                        },
                        located_with = samsaraRequest?.location?.located_with
                    },
                    sensors = new Sensors
                    {
                        battery = samsaraRequest.sensors is null ? 0 :
                         !string.IsNullOrEmpty(samsaraRequest.sensors.battery)
                         ? float.Parse(samsaraRequest.sensors.battery, CultureInfo.InvariantCulture.NumberFormat) : 0,

                         temperature = samsaraRequest.sensors is null || samsaraRequest.sensors.temperature <= 0
                        ? null : (float) samsaraRequest.sensors?.temperature ,
                    },
                    imei = samsaraRequest.imei

                };
            }
            else
            {
                matrackRequest = JsonConvert.DeserializeObject<MatrackRequest>(myQueueItem);

            }
            matrackRequests.Add(matrackRequest);

            return matrackRequests;
        }

        private List<MatrackRequest> FilterBatchRequest(string myQueueItem)
        {
            var matrackRequestArray = JsonConvert.DeserializeObject<MatrackRequestData>(myQueueItem);
            return matrackRequestArray.data;
        }


    }
}

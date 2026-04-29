using e4scoreDataIngestionFunctionApp.Models.RequestModels;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using e4scoreDataIngestionFunctionApp.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;
using e4scoreDataIngestionFunctionApp.Models.Enum;
using System.Globalization;
using System.Collections.Generic;
using e4scoreDataIngestionFunctionApp.Models;
using e4scoreDataIngestionFunctionApp.Services;
using Location = e4scoreDataIngestionFunctionApp.Models.RequestModels.Location;
using SysTask = System.Threading.Tasks.Task;

namespace e4scoreDataIngestionFunctionApp
{
    public class MatrackQueueTrigger
    {
        private readonly IProcessDeviceInfo _processDeviceInfo;
        private readonly IMessageSegmentationQueue _segmentationQueue;
        private readonly IRebuildQueue _matrack;
        private readonly ILogger<MatrackQueueTrigger> _logger;

        public MatrackQueueTrigger(
            IProcessDeviceInfo processDeviceInfo,
            IMessageSegmentationQueue segmentationQueue,
            IRebuildQueue matrack,
            ILogger<MatrackQueueTrigger> logger)
        {
            _processDeviceInfo = processDeviceInfo;
            _segmentationQueue = segmentationQueue;
            _matrack = matrack;
            _logger = logger;
        }

        [Function(ApplicationSettings.MatrackFunctionName)]
        public async SysTask Run(
            [ServiceBusTrigger(ApplicationSettings.MatrackQueueName, Connection = ApplicationSettings.MaTrackQueueConnection)]
            string myQueueItem,
            CancellationToken ct)
        {
            try
            {
                _logger.LogInformation("Packet: {Packet}", myQueueItem);
                var watch = System.Diagnostics.Stopwatch.StartNew();

                var matrackRequests = FilterRequests(myQueueItem);

                _logger.LogWarning("[Prod] Matrack Function App Start IMEI: {Imei}", matrackRequests[0].imei);

                int count = 1;
                foreach (var matrackRequest in matrackRequests)
                {
                    _logger.LogWarning("[Prod] Executing message no: {Count} IMEI: {Imei}", count, matrackRequest.imei);
                    if (matrackRequest.timestamp.Year >= DateTime.UtcNow.Year)
                    {
                        if (matrackRequest.location is not null && matrackRequest.location.located_with is not null)
                        {
                            var locatedWith = matrackRequest.location.located_with.ToLowerInvariant();
                            if (locatedWith.Contains("samsara") || locatedWith.Contains("roadready"))
                            {
                                await _matrack.SendAsync(matrackRequest, _logger);
                            }
                        }
                        await _segmentationQueue.SendAsync(matrackRequest, _logger);
                    }
                    count++;
                }

                watch.Stop();
                _logger.LogWarning(
                    "Function Execution Time (success): {ElapsedMs}ms {ElapsedSec}s",
                    watch.ElapsedMilliseconds, watch.Elapsed.Seconds);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception in {Function}", nameof(MatrackQueueTrigger));
                throw;
            }
        }

        private List<MatrackRequest> FilterRequests(string myQueueItem)
        {
            if (myQueueItem.Contains("data"))
                return FilterBatchRequest(myQueueItem);

            return FilterSingleRequest(myQueueItem);
        }

        private List<MatrackRequest> FilterSingleRequest(string myQueueItem)
        {
            var matrackRequest = new MatrackRequest();
            if (myQueueItem.Contains("samsara"))
            {
                var samsaraRequest = JsonConvert.DeserializeObject<Samsara>(myQueueItem);
                matrackRequest = new MatrackRequest
                {
                    timestamp = samsaraRequest!.timestamp,
                    location = new Location
                    {
                        primary = new Primary
                        {
                            latitude = samsaraRequest.location.primary.latitude,
                            longitude = samsaraRequest.location.primary.longitude,
                        },
                        located_with = samsaraRequest.location?.located_with
                    },
                    sensors = new Sensors
                    {
                        battery = samsaraRequest.sensors is null ? 0 :
                            !string.IsNullOrEmpty(samsaraRequest.sensors.battery)
                                ? float.Parse(samsaraRequest.sensors.battery, CultureInfo.InvariantCulture.NumberFormat) : 0,
                        temperature = samsaraRequest.sensors is null || samsaraRequest.sensors.temperature <= 0
                            ? null : (float)samsaraRequest.sensors.temperature,
                    },
                    imei = samsaraRequest.imei
                };
            }
            else
            {
                matrackRequest = JsonConvert.DeserializeObject<MatrackRequest>(myQueueItem)!;
            }

            return new List<MatrackRequest> { matrackRequest };
        }

        private List<MatrackRequest> FilterBatchRequest(string myQueueItem)
        {
            var matrackRequestArray = JsonConvert.DeserializeObject<MatrackRequestData>(myQueueItem)!;
            return matrackRequestArray.data;
        }
    }
}


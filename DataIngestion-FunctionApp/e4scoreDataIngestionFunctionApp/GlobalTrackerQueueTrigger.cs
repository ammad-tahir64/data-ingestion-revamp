using System;
using System.Threading;
using System.Threading.Tasks;
using e4scoreDataIngestionFunctionApp.Interfaces;
using e4scoreDataIngestionFunctionApp.Models.RequestModels;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace e4scoreDataIngestionFunctionApp
{
    public class GlobalTrackerQueueTrigger
    {
        private readonly IProcessDeviceInfo _processDeviceInfo;
        private readonly ILogger<GlobalTrackerQueueTrigger> _logger;

        public GlobalTrackerQueueTrigger(
            IProcessDeviceInfo processDeviceInfo,
            ILogger<GlobalTrackerQueueTrigger> logger)
        {
            _processDeviceInfo = processDeviceInfo;
            _logger = logger;
        }

        // Disabled via app setting: AzureWebJobs.GlobalTrackerQueueTrigger.Disabled = true
        [Function("GlobalTrackerQueueTrigger")]
        public async Task Run(
            [ServiceBusTrigger("globaltracker", Connection = Models.Enum.ApplicationSettings.MaTrackQueueConnection)]            string myQueueItem,
            CancellationToken ct)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            _logger.LogWarning("Global Tracker Function App Start");

            var globalTracker = JsonConvert.DeserializeObject<GlobalTracker>(myQueueItem)!;

            var matrackRequest = new MatrackRequest
            {
                timestamp = globalTracker.whenCreated,
                location = new Location
                {
                    primary = new Primary
                    {
                        latitude = globalTracker.latitude,
                        longitude = globalTracker.longitude,
                    }
                },
                imei = globalTracker.assetId
            };

            if (matrackRequest.timestamp.Year >= DateTime.UtcNow.Year)
            {
                await _processDeviceInfo.Process(matrackRequest, _logger);
            }

            watch.Stop();
            _logger.LogWarning(
                "Global Tracker Execution Time (success): {ElapsedMs}ms {ElapsedSec}s",
                watch.ElapsedMilliseconds, watch.Elapsed.Seconds);
        }
    }
}


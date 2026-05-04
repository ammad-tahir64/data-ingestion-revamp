using System;
using System.Threading;
using System.Threading.Tasks;
using e4scoreDataIngestionFunctionApp.Interfaces;
using e4scoreDataIngestionFunctionApp.Models.RequestModels;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using e4scoreDataIngestionFunctionApp.Models.Enum;

namespace e4scoreDataIngestionFunctionApp
{
    public class GlobalTrackerTrigger
    {
        private readonly IProcessDeviceInfo _processDeviceInfo;
        private readonly ILogger<GlobalTrackerTrigger> _logger;

        public GlobalTrackerTrigger(
            IProcessDeviceInfo processDeviceInfo,
            ILogger<GlobalTrackerTrigger> logger)
        {
            _processDeviceInfo = processDeviceInfo;
            _logger = logger;
        }

        // Disabled via app setting: AzureWebJobs.GlobalTrackerTrigger.Disabled = true
        [Function(ApplicationSettings.GlobalTrackerFunctionName)]
        public async Task Run(
            [ServiceBusTrigger(ApplicationSettings.GlobalTrackerQueueName, Connection = ApplicationSettings.MaTrackQueueConnection)]
            string myQueueItem,
            CancellationToken ct)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            _logger.LogWarning("[{Function}] Start", ApplicationSettings.GlobalTrackerFunctionName);

            var globalTracker = JsonConvert.DeserializeObject<GlobalTracker>(myQueueItem)!;

            var request = new MatrackRequest
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

            if (request.timestamp.Year >= DateTime.UtcNow.Year)
            {
                await _processDeviceInfo.Process(request, _logger);
            }

            watch.Stop();
            _logger.LogWarning("[{Function}] Done — {ElapsedMs}ms",
                ApplicationSettings.GlobalTrackerFunctionName, watch.ElapsedMilliseconds);
        }
    }
}

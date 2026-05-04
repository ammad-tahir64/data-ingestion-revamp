using System;
using System.Threading;
using System.Threading.Tasks;
using e4scoreDataIngestionFunctionApp.Interfaces;
using e4scoreDataIngestionFunctionApp.Models.RequestModels;
using e4scoreDataIngestionFunctionApp.Models.Enum;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace e4scoreDataIngestionFunctionApp
{
    public class MessageSegmentationTrigger
    {
        private readonly IProcessDeviceInfo _processDeviceInfo;
        private readonly ILogger<MessageSegmentationTrigger> _logger;

        public MessageSegmentationTrigger(
            IProcessDeviceInfo processDeviceInfo,
            ILogger<MessageSegmentationTrigger> logger)
        {
            _processDeviceInfo = processDeviceInfo;
            _logger = logger;
        }

        [Function(ApplicationSettings.MessageSegmentationFunctionName)]
        public async Task Run(
            [ServiceBusTrigger(ApplicationSettings.MessageSegmentationQueueName, Connection = ApplicationSettings.MaTrackQueueConnection)]
            string myQueueItem,
            CancellationToken ct)
        {
            _logger.LogInformation("Packet: {Packet}", myQueueItem);
            var watch = System.Diagnostics.Stopwatch.StartNew();

            var request = JsonConvert.DeserializeObject<MatrackRequest>(myQueueItem)!;

            _logger.LogWarning("[{Function}] Start — IMEI: {Imei}",
                ApplicationSettings.MessageSegmentationFunctionName, request.imei);

            if (request.timestamp.Year >= DateTime.UtcNow.Year)
            {
                await _processDeviceInfo.Process(request, _logger);
            }

            watch.Stop();
            _logger.LogWarning("[{Function}] Done — {ElapsedMs}ms",
                ApplicationSettings.MessageSegmentationFunctionName, watch.ElapsedMilliseconds);
        }
    }
}


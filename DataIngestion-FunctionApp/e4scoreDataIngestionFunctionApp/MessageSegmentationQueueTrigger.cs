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
    public class MessageSegmentationQueueTrigger
    {
        private readonly IProcessDeviceInfo _processDeviceInfo;
        private readonly ILogger<MessageSegmentationQueueTrigger> _logger;

        public MessageSegmentationQueueTrigger(
            IProcessDeviceInfo processDeviceInfo,
            ILogger<MessageSegmentationQueueTrigger> logger)
        {
            _processDeviceInfo = processDeviceInfo;
            _logger = logger;
        }

        [Function("MessageSegmentationQueueTrigger")]
        public async Task Run(
            [ServiceBusTrigger(ApplicationSettings.MessageSegmentationQueueName, Connection = "MaTrackQueueConnection")]
            string myQueueItem,
            CancellationToken ct)
        {
            _logger.LogInformation("Packet: {Packet}", myQueueItem);
            var watch = System.Diagnostics.Stopwatch.StartNew();

            var matrackRequest = JsonConvert.DeserializeObject<MatrackRequest>(myQueueItem)!;

            _logger.LogWarning("Message Segmentation Function App Start IMEI: {Imei}", matrackRequest.imei);

            if (matrackRequest.timestamp.Year >= DateTime.UtcNow.Year)
            {
                await _processDeviceInfo.Process(matrackRequest, _logger);
            }

            watch.Stop();
            _logger.LogWarning(
                "Message Segmentation Execution Time (success): {ElapsedMs}ms {ElapsedSec}s",
                watch.ElapsedMilliseconds, watch.Elapsed.Seconds);
        }
    }
}


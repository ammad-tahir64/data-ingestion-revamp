using System;
using System.Threading.Tasks;
using e4scoreDataIngestionFunctionApp.Interfaces;
using e4scoreDataIngestionFunctionApp.Models.RequestModels;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace e4scoreDataIngestionFunctionApp
{
    public class MessageSegmentationQueueTrigger
    {
        private readonly IProcessDeviceInfo _processDeviceInfo;
        public MessageSegmentationQueueTrigger(IProcessDeviceInfo processDeviceInfo)
        {
            _processDeviceInfo = processDeviceInfo;
        }
        [FunctionName("MessageSegmentationQueueTrigger")]
        public async Task Run([ServiceBusTrigger("messagesegmentation", Connection = "MaTrackQueueConnection")]string myQueueItem, ILogger log)
        {
            log.LogInformation($"Packet : {myQueueItem} -----------------------------------------------------");
            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();

            var matrackRequest = JsonConvert.DeserializeObject<MatrackRequest>(myQueueItem);

            log.LogWarning($"Message Segmentation Function App Start IMEI : {matrackRequest.imei} -----------------------------------------------------");


            if (matrackRequest.timestamp.Year >= DateTime.Now.Year)
            {
                var result = await _processDeviceInfo.Process(matrackRequest, log);
            }

            watch.Stop();
            log.LogWarning($"Message Segmentation Function Execution Time (success): {watch.ElapsedMilliseconds} ms {watch.Elapsed.Seconds} sec ------------------------------------------------------");
        }
    }
}

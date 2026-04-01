using System;
using System.Threading.Tasks;
using e4scoreDataIngestionFunctionApp.Interfaces;
using e4scoreDataIngestionFunctionApp.Models.Enum;
using e4scoreDataIngestionFunctionApp.Models.RequestModels;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace e4scoreDataIngestionFunctionApp
{
    public class GlobalTrackerQueueTrigger
    {
        private readonly IProcessDeviceInfo _processDeviceInfo;
        public GlobalTrackerQueueTrigger(IProcessDeviceInfo processDeviceInfo)
        {
            _processDeviceInfo = processDeviceInfo;
        }
        [FunctionName("GlobalTrackerQueueTrigger")]
        public async Task Run([ServiceBusTrigger("globaltracker", Connection = ApplicationSettings.MaTrackQueueConnection),Disable()]string myQueueItem, ILogger log)
        {
            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            log.LogWarning("Global Tracker Function App Start -----------------------------------------------------");
            var globalTracker = JsonConvert.DeserializeObject<GlobalTracker>(myQueueItem);

            var matrackRequest = new MatrackRequest 
            {
                timestamp = globalTracker.whenCreated,
                location = new Location 
                { 
                    primary = new Primary { 
                    latitude = globalTracker.latitude,
                    longitude = globalTracker.longitude,
                    } 
                },
                imei = globalTracker.assetId
                
            };

            if (matrackRequest.timestamp.Year >= DateTime.Now.Year)
            {
                var result = await _processDeviceInfo.Process(matrackRequest, log);
            }


            watch.Stop();
            log.LogWarning($"Function Execution Time (success): {watch.ElapsedMilliseconds} ms {watch.Elapsed.Seconds} sec --------------------------------");
        }
    }

}

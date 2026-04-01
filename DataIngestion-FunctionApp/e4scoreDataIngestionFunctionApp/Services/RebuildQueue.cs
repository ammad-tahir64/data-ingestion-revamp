using Azure.Messaging.ServiceBus;
using e4scoreDataIngestionFunctionApp.Models.Enum;
using e4scoreDataIngestionFunctionApp.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using e4scoreDataIngestionFunctionApp.Models.RequestModels;

namespace e4scoreDataIngestionFunctionApp.Services
{
    public class RebuildQueue : IRebuildQueue
    {
        public async Task<bool> SendAsync(MatrackRequest matrackRequest, ILogger log)
        {

            ServiceBusClient serviceBusClient = new ServiceBusClient(Environment.GetEnvironmentVariable(ApplicationSettings.MaTrackQueueConnectionRebuild));
            var sender = serviceBusClient.CreateSender("matrack");

            try
            {
                var messageToBe = JsonSerializer.Serialize(matrackRequest);
                var message = new ServiceBusMessage(messageToBe);
                // Use the producer client to send the batch of messages to the service bus queue
                await sender.SendMessageAsync(message);
                log.LogWarning($"Message has been sent to the queue...");
                return true;
            }
            catch (Exception ex)
            {
                log.LogError($"Exception on sending matrack Queue: {matrackRequest.imei} Exception {ex.Message} --------------------------------");
                throw ex;
            }
            finally
            {
                // Calling DisposeAsync on client types is required to ensure network
                // resources and other unmanaged objects are properly cleaned up
                await sender.DisposeAsync();
                await serviceBusClient.DisposeAsync();
            }
        }
    }
}

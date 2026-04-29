using Azure.Messaging.ServiceBus;
using e4scoreDataIngestionFunctionApp.Interfaces;
using e4scoreDataIngestionFunctionApp.Models;
using e4scoreDataIngestionFunctionApp.Models.Enum;
using e4scoreDataIngestionFunctionApp.Models.RequestModels;
using Microsoft.Extensions.Logging;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace e4scoreDataIngestionFunctionApp.Services
{
    public class DeviceProcessingQueue : IDeviceProcessingQueue
    {
        public async Task<bool> SendAsync(DeviceProcessing deviceProcessing, ILogger log)
        {

            ServiceBusClient serviceBusClient = new ServiceBusClient(Environment.GetEnvironmentVariable(ApplicationSettings.MaTrackQueueConnection));
            var sender = serviceBusClient.CreateSender(ApplicationSettings.DeviceProcessingQueueName);
            try
            {
                var messageToBe = JsonSerializer.Serialize(deviceProcessing);
                var message = new ServiceBusMessage(messageToBe);
                // Use the producer client to send the batch of messages to the service bus queue
                await sender.SendMessageAsync(message);
                log.LogWarning($"Message has been sent to the queue...");
                return true;
            }
            catch (Exception ex)
            {
                log.LogError($"Exception on sending deviceprocessing Queue: {deviceProcessing.IMEI} Exception {ex.Message} --------------------------------");
                throw;
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

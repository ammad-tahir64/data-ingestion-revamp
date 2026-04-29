using Azure.Messaging.ServiceBus;
using e4scoreDataIngestionFunctionApp.Interfaces;
using e4scoreDataIngestionFunctionApp.Models;
using e4scoreDataIngestionFunctionApp.Models.Enum;
using Microsoft.Extensions.Logging;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace e4scoreDataIngestionFunctionApp.Services
{
    /// <summary>
    /// Sends <see cref="DeviceProcessing"/> messages to the EAI integration queue
    /// (<c>e4score-eai-queue</c>).
    /// </summary>
    public sealed class EaiQueueSender : IEaiQueueSender, IAsyncDisposable
    {
        private readonly ServiceBusClient _client;
        private readonly ServiceBusSender _sender;

        public EaiQueueSender()
        {
            _client = new ServiceBusClient(
                Environment.GetEnvironmentVariable(ApplicationSettings.E4scoreEaiQueueConnection)
                ?? throw new InvalidOperationException($"Environment variable '{ApplicationSettings.E4scoreEaiQueueConnection}' is not set."));
            _sender = _client.CreateSender(ApplicationSettings.EaiQueueName);
        }

        public async Task<bool> SendAsync(DeviceProcessing deviceProcessing, ILogger logger)
        {
            try
            {
                var message = new ServiceBusMessage(JsonSerializer.Serialize(deviceProcessing));
                await _sender.SendMessageAsync(message);
                logger.LogInformation("Message sent to {Queue}", ApplicationSettings.EaiQueueName);
                return true;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to send to {Queue} — IMEI: {Imei}",
                    ApplicationSettings.EaiQueueName, deviceProcessing.IMEI);
                throw;
            }
        }

        public async ValueTask DisposeAsync()
        {
            await _sender.DisposeAsync();
            await _client.DisposeAsync();
        }
    }
}

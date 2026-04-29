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
    /// Sends enriched <see cref="DeviceProcessing"/> messages to the
    /// <c>e4.processing.business-enrichment</c> queue.
    /// </summary>
    public sealed class BusinessEnrichmentQueueSender : IBusinessEnrichmentQueueSender, IAsyncDisposable
    {
        private readonly ServiceBusClient _client;
        private readonly ServiceBusSender _sender;

        public BusinessEnrichmentQueueSender()
        {
            _client = new ServiceBusClient(
                Environment.GetEnvironmentVariable(ApplicationSettings.MaTrackQueueConnection)
                ?? throw new InvalidOperationException($"Environment variable '{ApplicationSettings.MaTrackQueueConnection}' is not set."));
            _sender = _client.CreateSender(ApplicationSettings.BusinessEnrichmentQueueName);
        }

        public async Task<bool> SendAsync(DeviceProcessing deviceProcessing, ILogger logger)
        {
            try
            {
                var message = new ServiceBusMessage(JsonSerializer.Serialize(deviceProcessing));
                await _sender.SendMessageAsync(message);
                logger.LogInformation("Message sent to {Queue}", ApplicationSettings.BusinessEnrichmentQueueName);
                return true;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to send to {Queue} — IMEI: {Imei}",
                    ApplicationSettings.BusinessEnrichmentQueueName, deviceProcessing.IMEI);
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

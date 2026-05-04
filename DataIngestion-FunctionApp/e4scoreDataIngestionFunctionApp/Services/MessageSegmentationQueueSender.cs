using Azure.Messaging.ServiceBus;
using e4scoreDataIngestionFunctionApp.Interfaces;
using e4scoreDataIngestionFunctionApp.Models.Enum;
using e4scoreDataIngestionFunctionApp.Models.RequestModels;
using Microsoft.Extensions.Logging;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace e4scoreDataIngestionFunctionApp.Services
{
    /// <summary>
    /// Sends <see cref="MatrackRequest"/> messages to the
    /// <c>e4.processing.message-segmentation</c> queue.
    /// </summary>
    public sealed class MessageSegmentationQueueSender : IMessageSegmentationQueueSender, IAsyncDisposable
    {
        private readonly ServiceBusClient _client;
        private readonly ServiceBusSender _sender;

        public MessageSegmentationQueueSender()
        {
            _client = new ServiceBusClient(
                Environment.GetEnvironmentVariable(ApplicationSettings.MaTrackQueueConnection)
                ?? throw new InvalidOperationException($"Environment variable '{ApplicationSettings.MaTrackQueueConnection}' is not set."));
            _sender = _client.CreateSender(ApplicationSettings.MessageSegmentationQueueName);
        }

        public async Task<bool> SendAsync(MatrackRequest request, ILogger logger)
        {
            try
            {
                var message = new ServiceBusMessage(JsonSerializer.Serialize(request));
                await _sender.SendMessageAsync(message);
                logger.LogInformation("Message sent to {Queue}", ApplicationSettings.MessageSegmentationQueueName);
                return true;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to send to {Queue} — IMEI: {Imei}",
                    ApplicationSettings.MessageSegmentationQueueName, request.imei);
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

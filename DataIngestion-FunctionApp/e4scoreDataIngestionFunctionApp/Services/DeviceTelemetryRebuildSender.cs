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
    /// Re-publishes <see cref="MatrackRequest"/> messages back to the device-telemetry
    /// queue (<c>e4.ingestion.device-telemetry</c>) using the rebuild connection string.
    /// Used for Samsara/RoadReady re-processing flows.
    /// </summary>
    public sealed class DeviceTelemetryRebuildSender : IDeviceTelemetryRebuildSender, IAsyncDisposable
    {
        private readonly ServiceBusClient _client;
        private readonly ServiceBusSender _sender;

        public DeviceTelemetryRebuildSender()
        {
            _client = new ServiceBusClient(
                Environment.GetEnvironmentVariable(ApplicationSettings.MaTrackQueueConnectionRebuild)
                ?? throw new InvalidOperationException($"Environment variable '{ApplicationSettings.MaTrackQueueConnectionRebuild}' is not set."));
            _sender = _client.CreateSender(ApplicationSettings.DeviceTelemetryQueueName);
        }

        public async Task<bool> SendAsync(MatrackRequest request, ILogger logger)
        {
            try
            {
                var message = new ServiceBusMessage(JsonSerializer.Serialize(request));
                await _sender.SendMessageAsync(message);
                logger.LogInformation("Message sent to {Queue}", ApplicationSettings.DeviceTelemetryQueueName);
                return true;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to send to {Queue} — IMEI: {Imei}",
                    ApplicationSettings.DeviceTelemetryQueueName, request.imei);
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

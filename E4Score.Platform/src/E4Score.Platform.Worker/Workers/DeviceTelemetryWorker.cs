using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Processor;
using E4Score.Platform.Contracts.Events;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using System.Threading.Channels;

namespace E4Score.Platform.Worker.Workers;

/// <summary>
/// Stage 1 — Ingestion.
/// Consumes raw telemetry events from Azure Event Hub using EventProcessorClient
/// (supports partition ownership, consumer groups, and checkpointing).
/// Writes deserialized RawTelemetryMessage objects into an in-memory Channel
/// for the MessageSegmentationWorker to consume.
/// </summary>
public sealed class DeviceTelemetryWorker : BackgroundService
{
    private readonly EventProcessorClient _processor;
    private readonly ChannelWriter<RawTelemetryMessage> _channelWriter;
    private readonly ILogger<DeviceTelemetryWorker> _logger;

    public DeviceTelemetryWorker(
        EventProcessorClient processor,
        Channel<RawTelemetryMessage> channel,
        ILogger<DeviceTelemetryWorker> logger)
    {
        _processor = processor;
        _channelWriter = channel.Writer;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken ct)
    {
        _processor.ProcessEventAsync += ProcessEventAsync;
        _processor.ProcessErrorAsync += ProcessErrorAsync;

        await _processor.StartProcessingAsync(ct);
        _logger.LogInformation("DeviceTelemetryWorker started — consuming from Event Hub");

        try
        {
            await Task.Delay(Timeout.Infinite, ct);
        }
        finally
        {
            await _processor.StopProcessingAsync();
            _processor.ProcessEventAsync -= ProcessEventAsync;
            _processor.ProcessErrorAsync -= ProcessErrorAsync;
            _channelWriter.Complete();
            _logger.LogInformation("DeviceTelemetryWorker stopped");
        }
    }

    private async Task ProcessEventAsync(ProcessEventArgs args)
    {
        if (args.CancellationToken.IsCancellationRequested) return;
        if (!args.HasEvent) return;

        try
        {
            var body = args.Data.Body.ToArray();
            var raw = JsonSerializer.Deserialize<RawTelemetryMessage>(body);
            if (raw is null)
            {
                _logger.LogWarning("Received null payload from Event Hub partition {Partition}",
                    args.Partition.PartitionId);
                await args.UpdateCheckpointAsync(args.CancellationToken);
                return;
            }

            // Augment with Event Hub metadata
            var withMeta = raw with
            {
                PartitionId = args.Partition.PartitionId,
                SequenceNumber = args.Data.SequenceNumber,
                MessageId = raw.MessageId ?? $"{raw.Imei}:{raw.Timestamp.Ticks}"
            };

            await _channelWriter.WriteAsync(withMeta, args.CancellationToken);
            await args.UpdateCheckpointAsync(args.CancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing Event Hub message on partition {Partition}",
                args.Partition.PartitionId);
        }
    }

    private Task ProcessErrorAsync(ProcessErrorEventArgs args)
    {
        _logger.LogError(args.Exception,
            "Event Hub processor error on partition {Partition} in operation {Operation}",
            args.PartitionId, args.Operation);
        return Task.CompletedTask;
    }
}

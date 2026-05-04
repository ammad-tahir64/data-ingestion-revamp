using Azure.Messaging.ServiceBus;
using E4Score.Platform.Contracts.Events;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using System.Threading.Channels;

namespace E4Score.Platform.Worker.Workers;

/// <summary>
/// Reads messages from the Azure Service Bus dead-letter queue and resubmits
/// them to the raw telemetry channel for reprocessing.
///
/// Retry schedule: messages are only eligible for reprocessing after their
/// ScheduledEnqueueTime (set to now + 30 min by ServiceBusDeadLetterService).
/// After MaxDeliveryCount (default 10) retries, Service Bus moves the message
/// to the queue's own built-in DLQ — visible in Service Bus Explorer.
///
/// This service runs on a 60-second poll interval to avoid competing with
/// the main pipeline for channel capacity.
/// </summary>
public sealed class DeadLetterReprocessorService : BackgroundService
{
    private readonly ServiceBusClient _sbClient;
    private readonly ChannelWriter<RawTelemetryMessage> _rawWriter;
    private readonly ILogger<DeadLetterReprocessorService> _logger;
    private readonly string _queueName;
    private readonly TimeSpan _pollInterval = TimeSpan.FromSeconds(60);
    private const int MaxBatchSize = 50;

    public DeadLetterReprocessorService(
        ServiceBusClient sbClient,
        Channel<RawTelemetryMessage> rawChannel,
        ILogger<DeadLetterReprocessorService> logger,
        string queueName)
    {
        _sbClient = sbClient;
        _rawWriter = rawChannel.Writer;
        _logger = logger;
        _queueName = queueName;
    }

    protected override async Task ExecuteAsync(CancellationToken ct)
    {
        _logger.LogInformation("DeadLetterReprocessorService started — polling {Queue} every {Interval}s",
            _queueName, _pollInterval.TotalSeconds);

        await using var receiver = _sbClient.CreateReceiver(
            _queueName,
            new ServiceBusReceiverOptions
            {
                ReceiveMode = ServiceBusReceiveMode.PeekLock
            });

        using var timer = new PeriodicTimer(_pollInterval);
        while (await timer.WaitForNextTickAsync(ct))
        {
            try
            {
                var messages = await receiver.ReceiveMessagesAsync(MaxBatchSize, maxWaitTime: TimeSpan.FromSeconds(5), ct);
                if (messages.Count == 0) continue;

                _logger.LogInformation("DeadLetterReprocessorService: received {Count} messages for reprocessing", messages.Count);

                foreach (var msg in messages)
                {
                    try
                    {
                        var raw = JsonSerializer.Deserialize<RawTelemetryMessage>(msg.Body.ToArray());
                        if (raw is null)
                        {
                            await receiver.DeadLetterMessageAsync(msg, "DeserializationFailed",
                                "Message body could not be deserialized to RawTelemetryMessage", ct);
                            continue;
                        }

                        await _rawWriter.WriteAsync(raw, ct);
                        await receiver.CompleteMessageAsync(msg, ct);

                        _logger.LogDebug("Requeued DLQ message for IMEI {Imei}", raw.Imei);
                    }
                    catch (OperationCanceledException) { throw; }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Failed to reprocess DLQ message — abandoning");
                        await receiver.AbandonMessageAsync(msg, cancellationToken: ct);
                    }
                }
            }
            catch (OperationCanceledException) { break; }
            catch (Exception ex)
            {
                _logger.LogError(ex, "DeadLetterReprocessorService poll failed — will retry in {Interval}s",
                    _pollInterval.TotalSeconds);
            }
        }

        _logger.LogInformation("DeadLetterReprocessorService stopped");
    }
}

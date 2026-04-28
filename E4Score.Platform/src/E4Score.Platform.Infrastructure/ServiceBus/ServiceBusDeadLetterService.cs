using Azure.Messaging.ServiceBus;
using E4Score.Platform.Contracts.Events;
using E4Score.Platform.Contracts.Interfaces;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace E4Score.Platform.Infrastructure.ServiceBus;

/// <summary>
/// Sends failed telemetry messages to the Azure Service Bus dead-letter queue.
/// Messages include failure metadata and are scheduled for retry after 30 minutes.
/// </summary>
public sealed class ServiceBusDeadLetterService : IDeadLetterService, IAsyncDisposable
{
    private readonly ServiceBusSender _sender;
    private readonly ILogger<ServiceBusDeadLetterService> _logger;

    public ServiceBusDeadLetterService(ServiceBusClient client, string queueName,
        ILogger<ServiceBusDeadLetterService> logger)
    {
        _sender = client.CreateSender(queueName);
        _logger = logger;
    }

    public async Task SendToDeadLetterAsync(
        RawTelemetryMessage message,
        Exception exception,
        int retryCount,
        CancellationToken ct = default)
    {
        var body = JsonSerializer.SerializeToUtf8Bytes(message);
        var sbMessage = new ServiceBusMessage(body)
        {
            Subject = "ProcessingFailure",
            ContentType = "application/json",
            ScheduledEnqueueTime = DateTimeOffset.UtcNow.AddMinutes(30)
        };

        sbMessage.ApplicationProperties["OriginalImei"] = message.Imei;
        sbMessage.ApplicationProperties["FailureReason"] = exception.Message;
        sbMessage.ApplicationProperties["ExceptionType"] = exception.GetType().FullName;
        sbMessage.ApplicationProperties["RetryCount"] = retryCount;
        sbMessage.ApplicationProperties["FailedAt"] = DateTimeOffset.UtcNow.ToString("O");
        sbMessage.ApplicationProperties["OriginalEventTime"] = message.Timestamp.ToString("O");

        await _sender.SendMessageAsync(sbMessage, ct);
        _logger.LogWarning("Message for IMEI {Imei} sent to dead-letter queue after {RetryCount} retries",
            message.Imei, retryCount);
    }

    public async ValueTask DisposeAsync() => await _sender.DisposeAsync();
}

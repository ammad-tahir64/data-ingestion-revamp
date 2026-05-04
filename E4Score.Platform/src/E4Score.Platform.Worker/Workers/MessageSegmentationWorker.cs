using E4Score.Platform.Contracts.Events;
using E4Score.Platform.Contracts.Interfaces;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading.Channels;

namespace E4Score.Platform.Worker.Workers;

/// <summary>
/// Stage 2 — Segmentation.
/// Reads RawTelemetryMessages from the ingestion channel, validates them,
/// deduplicates via Redis idempotency check, and passes valid messages
/// to the BusinessEnrichmentWorker through the enrichment channel.
///
/// Rejects messages with:
///   - Timestamp older than current year (matches existing validation logic)
///   - Duplicate messageId (Redis SETNX idempotency check)
/// </summary>
public sealed class MessageSegmentationWorker : BackgroundService
{
    private readonly ChannelReader<RawTelemetryMessage> _rawReader;
    private readonly ChannelWriter<RawTelemetryMessage> _validatedWriter;
    private readonly IIdempotencyService _idempotency;
    private readonly IDeadLetterService _deadLetter;
    private readonly ILogger<MessageSegmentationWorker> _logger;

    private static readonly TimeSpan IdempotencyWindow = TimeSpan.FromHours(24);

    public MessageSegmentationWorker(
        Channel<RawTelemetryMessage> rawChannel,
        Channel<RawTelemetryMessage> validatedChannel,
        IIdempotencyService idempotency,
        IDeadLetterService deadLetter,
        ILogger<MessageSegmentationWorker> logger)
    {
        _rawReader = rawChannel.Reader;
        _validatedWriter = validatedChannel.Writer;
        _idempotency = idempotency;
        _deadLetter = deadLetter;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken ct)
    {
        _logger.LogInformation("MessageSegmentationWorker started");

        await foreach (var message in _rawReader.ReadAllAsync(ct))
        {
            try
            {
                // Reject stale messages (year check matches existing business rule)
                if (message.Timestamp.Year < DateTime.UtcNow.Year)
                {
                    _logger.LogDebug("Stale message from IMEI {Imei} timestamp {Ts} — discarded",
                        message.Imei, message.Timestamp);
                    continue;
                }

                // Idempotency check — skip if already processed
                var messageId = message.MessageId ?? $"{message.Imei}:{message.Timestamp.Ticks}";
                if (!await _idempotency.TryClaimAsync(messageId, IdempotencyWindow, ct))
                {
                    _logger.LogDebug("Duplicate message {MessageId} — skipped", messageId);
                    continue;
                }

                await _validatedWriter.WriteAsync(message, ct);
            }
            catch (OperationCanceledException) { break; }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Segmentation error for IMEI {Imei}", message.Imei);
                await _deadLetter.SendToDeadLetterAsync(message, ex, retryCount: 0, ct);
            }
        }

        _validatedWriter.Complete();
        _logger.LogInformation("MessageSegmentationWorker stopped");
    }
}

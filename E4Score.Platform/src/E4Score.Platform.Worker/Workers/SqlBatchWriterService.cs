using E4Score.Platform.Contracts.Events;
using E4Score.Platform.Contracts.Interfaces;
using E4Score.Platform.Infrastructure.SqlServer.BulkOperations;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Threading.Channels;

namespace E4Score.Platform.Worker.Workers;

/// <summary>
/// Stage 4 — Persistence.
/// Reads EnrichedTelemetryEvents from the enrichment channel and flushes them to
/// SQL Server in micro-batches using SqlBulkCopy + Dapper MERGE.
///
/// Flush triggers:
///   - Batch size reaches BatchSize (default 500)
///   - Flush interval elapses (default 2 seconds)
///   - Channel is empty after draining (flush remainder immediately)
///
/// On batch failure: retry up to 3 times, then send all events in batch to DLQ.
/// </summary>
public sealed class SqlBatchWriterService : BackgroundService
{
    private readonly ChannelReader<EnrichedTelemetryEvent> _reader;
    private readonly SqlBatchWriter _batchWriter;
    private readonly IDeadLetterService _deadLetter;
    private readonly ILogger<SqlBatchWriterService> _logger;
    private readonly SqlBatchOptions _options;

    public SqlBatchWriterService(
        Channel<EnrichedTelemetryEvent> enrichedChannel,
        SqlBatchWriter batchWriter,
        IDeadLetterService deadLetter,
        IOptions<SqlBatchOptions> options,
        ILogger<SqlBatchWriterService> logger)
    {
        _reader = enrichedChannel.Reader;
        _batchWriter = batchWriter;
        _deadLetter = deadLetter;
        _options = options.Value;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken ct)
    {
        _logger.LogInformation(
            "SqlBatchWriterService started — batch size: {BatchSize}, flush interval: {Interval}s",
            _options.BatchSize, _options.FlushIntervalSeconds);

        var buffer = new List<EnrichedTelemetryEvent>(_options.BatchSize);
        using var flushTimer = new PeriodicTimer(TimeSpan.FromSeconds(_options.FlushIntervalSeconds));

        async Task FlushBufferAsync()
        {
            if (buffer.Count == 0) return;
            var batch = buffer.ToList();
            buffer.Clear();
            await FlushWithRetryAsync(batch, ct);
        }

        while (!ct.IsCancellationRequested)
        {
            // Try to fill the buffer within the flush interval
            var timerTask = flushTimer.WaitForNextTickAsync(ct).AsTask();

            while (buffer.Count < _options.BatchSize)
            {
                var readTask = _reader.WaitToReadAsync(ct).AsTask();
                var completedTask = await Task.WhenAny(readTask, timerTask);

                if (completedTask == timerTask) break;

                while (buffer.Count < _options.BatchSize && _reader.TryRead(out var item))
                    buffer.Add(item);

                if (!await readTask) goto channelComplete;
            }

            await FlushBufferAsync();
        }

        channelComplete:
        // Drain any remaining items before shutdown
        while (_reader.TryRead(out var remaining))
            buffer.Add(remaining);
        if (buffer.Count > 0)
            await FlushWithRetryAsync(buffer, CancellationToken.None);

        _logger.LogInformation("SqlBatchWriterService stopped");
    }

    private async Task FlushWithRetryAsync(List<EnrichedTelemetryEvent> batch, CancellationToken ct)
    {
        for (int attempt = 1; attempt <= _options.MaxRetryAttempts; attempt++)
        {
            try
            {
                await _batchWriter.FlushAsync(batch, ct);
                _logger.LogDebug("Flushed {Count} events on attempt {Attempt}", batch.Count, attempt);
                return;
            }
            catch (Exception ex) when (attempt < _options.MaxRetryAttempts)
            {
                var delay = TimeSpan.FromMilliseconds(500 * Math.Pow(2, attempt));
                _logger.LogWarning(ex, "Batch flush attempt {Attempt} failed — retrying in {Delay}ms",
                    attempt, delay.TotalMilliseconds);
                await Task.Delay(delay, ct);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Batch flush failed after {Attempts} attempts — sending {Count} events to DLQ",
                    _options.MaxRetryAttempts, batch.Count);
                // Convert to RawTelemetryMessage for DLQ (minimal representation)
                foreach (var e in batch)
                {
                    await _deadLetter.SendToDeadLetterAsync(
                        new RawTelemetryMessage
                        {
                            Imei = e.Imei,
                            Timestamp = e.SourceTimestamp,
                            MessageId = e.MessageId
                        }, ex, retryCount: _options.MaxRetryAttempts, ct);
                }
            }
        }
    }
}

public sealed class SqlBatchOptions
{
    public int BatchSize { get; init; } = 500;
    public int FlushIntervalSeconds { get; init; } = 2;
    public int MaxRetryAttempts { get; init; } = 3;
}

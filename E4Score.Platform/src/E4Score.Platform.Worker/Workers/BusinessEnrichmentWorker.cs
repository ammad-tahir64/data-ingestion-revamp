using E4Score.Platform.Contracts.Events;
using E4Score.Platform.Contracts.Interfaces;
using E4Score.Platform.Domain.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading.Channels;

namespace E4Score.Platform.Worker.Workers;

/// <summary>
/// Stage 3 — Enrichment.
/// Reads validated RawTelemetryMessages, runs them through the
/// TelemetryEnrichmentOrchestrator (geofence eval, dwell/excursion, reverse geocoding),
/// then writes EnrichedTelemetryEvents to the persistence channel.
///
/// All enrichment is cache-only (Redis) — no SQL Server calls on this path.
/// </summary>
public sealed class BusinessEnrichmentWorker : BackgroundService
{
    private readonly ChannelReader<RawTelemetryMessage> _validatedReader;
    private readonly ChannelWriter<EnrichedTelemetryEvent> _enrichedWriter;
    private readonly TelemetryEnrichmentOrchestrator _orchestrator;
    private readonly IDeadLetterService _deadLetter;
    private readonly ILogger<BusinessEnrichmentWorker> _logger;

    public BusinessEnrichmentWorker(
        Channel<RawTelemetryMessage> validatedChannel,
        Channel<EnrichedTelemetryEvent> enrichedChannel,
        TelemetryEnrichmentOrchestrator orchestrator,
        IDeadLetterService deadLetter,
        ILogger<BusinessEnrichmentWorker> logger)
    {
        _validatedReader = validatedChannel.Reader;
        _enrichedWriter = enrichedChannel.Writer;
        _orchestrator = orchestrator;
        _deadLetter = deadLetter;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken ct)
    {
        _logger.LogInformation("BusinessEnrichmentWorker started");

        await foreach (var raw in _validatedReader.ReadAllAsync(ct))
        {
            try
            {
                var enriched = await _orchestrator.EnrichAsync(raw, ct);
                if (enriched is not null)
                    await _enrichedWriter.WriteAsync(enriched, ct);
            }
            catch (OperationCanceledException) { break; }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Enrichment error for IMEI {Imei}", raw.Imei);
                await _deadLetter.SendToDeadLetterAsync(raw, ex, retryCount: 0, ct);
            }
        }

        _enrichedWriter.Complete();
        _logger.LogInformation("BusinessEnrichmentWorker stopped");
    }
}

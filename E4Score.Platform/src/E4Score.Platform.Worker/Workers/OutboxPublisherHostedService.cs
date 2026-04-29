using Azure.Messaging.ServiceBus;
using E4Score.Platform.Contracts.Events;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Data.SqlClient;
using Dapper;

namespace E4Score.Platform.Worker.Workers;

/// <summary>
/// Polls the outbox_message table every 5 seconds for unprocessed messages
/// and publishes them to the Azure Service Bus topic for downstream consumers.
///
/// Uses a poll-and-mark-processed pattern:
///   1. SELECT TOP 100 where processed_at IS NULL ORDER BY created_at
///   2. For each row, send to Service Bus
///   3. UPDATE processed_at = GETUTCDATE()
///
/// This ensures cross-service event delivery (e.g., dispatch system, analytics)
/// without coupling the hot path to Service Bus latency.
/// </summary>
public sealed class OutboxPublisherHostedService : BackgroundService
{
    private readonly string _connectionString;
    private readonly ServiceBusSender _sender;
    private readonly ILogger<OutboxPublisherHostedService> _logger;
    private readonly TimeSpan _pollInterval = TimeSpan.FromSeconds(5);
    private const int BatchSize = 100;

    public OutboxPublisherHostedService(
        string connectionString,
        ServiceBusClient sbClient,
        string topicName,
        ILogger<OutboxPublisherHostedService> logger)
    {
        _connectionString = connectionString;
        _sender = sbClient.CreateSender(topicName);
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken ct)
    {
        _logger.LogInformation("OutboxPublisherHostedService started — poll interval: {Interval}s", _pollInterval.TotalSeconds);

        using var timer = new PeriodicTimer(_pollInterval);
        while (await timer.WaitForNextTickAsync(ct))
        {
            try
            {
                await ProcessBatchAsync(ct);
            }
            catch (OperationCanceledException) { break; }
            catch (Exception ex)
            {
                _logger.LogError(ex, "OutboxPublisherHostedService batch failed — will retry in {Interval}s",
                    _pollInterval.TotalSeconds);
            }
        }

        await _sender.DisposeAsync();
        _logger.LogInformation("OutboxPublisherHostedService stopped");
    }

    private async Task ProcessBatchAsync(CancellationToken ct)
    {
        await using var connection = new SqlConnection(_connectionString);

        const string selectSql = """
            SELECT TOP (@BatchSize)
                id          AS Id,
                event_type  AS EventType,
                payload     AS Payload,
                created_at  AS CreatedAt
            FROM outbox_message
            WHERE processed_at IS NULL
            ORDER BY created_at ASC
            """;

        var rows = (await connection.QueryAsync<OutboxMessage>(
            new CommandDefinition(selectSql, new { BatchSize }, cancellationToken: ct))).AsList();

        if (rows.Count == 0) return;

        var sbMessages = rows.Select(row => new ServiceBusMessage(row.Payload)
        {
            MessageId = row.Id.ToString(),
            Subject = row.EventType,
            ContentType = "application/json"
        }).ToList();

        await _sender.SendMessagesAsync(sbMessages, ct);

        var ids = rows.Select(r => r.Id).ToList();
        const string updateSql = """
            UPDATE outbox_message
            SET processed_at = GETUTCDATE()
            WHERE id IN @Ids
            """;
        await connection.ExecuteAsync(new CommandDefinition(updateSql, new { Ids = ids }, cancellationToken: ct));

        _logger.LogDebug("OutboxPublisherHostedService: published {Count} outbox messages", rows.Count);
    }
}

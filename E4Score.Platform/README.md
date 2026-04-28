# E4Score Platform — Worker Service

## Architecture

Replaces the Azure Functions–based pipeline with a .NET 8 Worker Service that processes IoT telemetry through a **cache-first, batch-persist** pipeline.

### Pipeline Stages

```
IoT Devices ──► Azure Event Hub ──► DeviceTelemetryWorker
                                         │ Channel<RawTelemetryMessage>
                                    MessageSegmentationWorker (idempotency + validation)
                                         │ Channel<RawTelemetryMessage>
                                    BusinessEnrichmentWorker (geofence, dwell, excursion)
                                         │ Channel<EnrichedTelemetryEvent>
                                    SqlBatchWriterService (SqlBulkCopy + Dapper MERGE)
                                         │
                                    SQL Server (batch every 500 events or 2 seconds)
```

### Projects

| Project | Role |
|---|---|
| `E4Score.Platform.Contracts` | Shared DTOs, interfaces, constants |
| `E4Score.Platform.Domain` | Pure business logic (geofence evaluation, dwell/excursion calculation) |
| `E4Score.Platform.Infrastructure` | Dapper repositories, Redis services, SqlBulkCopy batch writer |
| `E4Score.Platform.Worker` | All hosted services + Program.cs DI wiring |

### Key Design Decisions

- **Dapper** for all SQL Server reads (device warmup, geocode lookup) — no EF Core
- **SqlBulkCopy** for all writes — batches 500 inserts into 1 TDS stream call
- **Redis** for device state — eliminates per-message SQL roundtrips
- **System.Threading.Channels** for in-process pipeline — no queue overhead between stages
- **NetTopologySuite** for in-memory geofence polygon evaluation

## Configuration

Copy `appsettings.json` and fill in:
- `ConnectionStrings:E4ScorePlatform` — SQL Server connection string
- `Redis:ConnectionString` — Redis connection string
- `Azure:EventHub:ConnectionString` — Event Hub namespace connection string
- `Azure:Storage:ConnectionString` — Azure Blob Storage (for Event Hub checkpoints)
- `Azure:ServiceBus:ConnectionString` — Service Bus (for dead-letter queue)

## Local Development

```bash
# Start dependencies
docker run -d -p 1433:1433 -e SA_PASSWORD=YourPassword123! -e ACCEPT_EULA=Y mcr.microsoft.com/mssql/server
docker run -d -p 6379:6379 redis

# Run the worker
cd src/E4Score.Platform.Worker
dotnet run
```

## Performance Target

| Metric | Azure Functions (current) | Worker Service (target) |
|---|---|---|
| SQL calls/sec | 10,000 | < 200 |
| Latency | 200–500ms | 15–40ms |
| Max throughput | ~1,200 msg/s | 10,000+ msg/s |

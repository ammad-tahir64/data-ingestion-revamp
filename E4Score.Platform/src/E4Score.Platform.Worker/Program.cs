using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Consumer;
using Azure.Storage.Blobs;
using E4Score.Platform.Contracts.Events;
using E4Score.Platform.Contracts.Interfaces;
using E4Score.Platform.Domain.Services;
using E4Score.Platform.Infrastructure.Redis;
using E4Score.Platform.Infrastructure.ServiceBus;
using E4Score.Platform.Infrastructure.SqlServer.BulkOperations;
using E4Score.Platform.Infrastructure.SqlServer.Repositories;
using E4Score.Platform.Worker.HealthChecks;
using E4Score.Platform.Worker.Workers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using StackExchange.Redis;
using System.Threading.Channels;
using Azure.Messaging.ServiceBus;

var builder = Host.CreateApplicationBuilder(args);

// ── Configuration ────────────────────────────────────────────────────────────

builder.Configuration
    .AddJsonFile("appsettings.json", optional: false)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .AddEnvironmentVariables("E4_");

// ── In-Process Channels (pipeline stages) ────────────────────────────────────

const int RawChannelCapacity = 10_000;
const int ValidatedChannelCapacity = 10_000;
const int EnrichedChannelCapacity = 5_000;

var rawChannel = Channel.CreateBounded<RawTelemetryMessage>(
    new BoundedChannelOptions(RawChannelCapacity)
    {
        FullMode = BoundedChannelFullMode.Wait,
        SingleReader = false,
        SingleWriter = false
    });

var validatedChannel = Channel.CreateBounded<RawTelemetryMessage>(
    new BoundedChannelOptions(ValidatedChannelCapacity)
    {
        FullMode = BoundedChannelFullMode.Wait,
        SingleReader = true,
        SingleWriter = true
    });

var enrichedChannel = Channel.CreateBounded<EnrichedTelemetryEvent>(
    new BoundedChannelOptions(EnrichedChannelCapacity)
    {
        FullMode = BoundedChannelFullMode.Wait,
        SingleReader = true,
        SingleWriter = true
    });

builder.Services.AddSingleton(rawChannel);
builder.Services.AddSingleton(validatedChannel);
builder.Services.AddSingleton(enrichedChannel);

// ── Redis ─────────────────────────────────────────────────────────────────────

builder.Services.AddSingleton<IConnectionMultiplexer>(_ =>
    ConnectionMultiplexer.Connect(
        builder.Configuration["Redis:ConnectionString"]
        ?? throw new InvalidOperationException("Redis:ConnectionString is required")));

builder.Services.AddSingleton<IDeviceStateCacheService, RedisDeviceStateCacheService>();
builder.Services.AddSingleton<IDeviceRuntimeStateCache, RedisDeviceRuntimeStateCache>();
builder.Services.AddSingleton<IIdempotencyService, RedisIdempotencyService>();
builder.Services.AddSingleton<RedisCachedGeocodingService>();
builder.Services.AddSingleton<IReverseGeocodingService>(sp =>
    sp.GetRequiredService<RedisCachedGeocodingService>());

// ── SQL Server (Dapper — only for warm-up reads and batch writes) ─────────────

var sqlConnectionString = builder.Configuration.GetConnectionString("E4ScorePlatform")
    ?? throw new InvalidOperationException("ConnectionStrings:E4ScorePlatform is required");

builder.Services.AddSingleton(sp => new DeviceRepository(sqlConnectionString,
    sp.GetRequiredService<ILogger<DeviceRepository>>()));
builder.Services.AddSingleton(sp => new GeofenceRepository(sqlConnectionString,
    sp.GetRequiredService<ILogger<GeofenceRepository>>()));
builder.Services.AddSingleton(sp => new GeocodeLocationRepository(sqlConnectionString,
    sp.GetRequiredService<ILogger<GeocodeLocationRepository>>()));
builder.Services.AddSingleton(sp => new SqlBatchWriter(sqlConnectionString,
    sp.GetRequiredService<ILogger<SqlBatchWriter>>()));

// ── Azure Service Bus (dead-letter queue) ─────────────────────────────────────

builder.Services.AddSingleton(_ =>
    new ServiceBusClient(builder.Configuration["Azure:ServiceBus:ConnectionString"]
        ?? throw new InvalidOperationException("Azure:ServiceBus:ConnectionString is required")));

builder.Services.AddSingleton<IDeadLetterService>(sp =>
    new ServiceBusDeadLetterService(
        sp.GetRequiredService<ServiceBusClient>(),
        builder.Configuration["Azure:ServiceBus:DeadLetterQueue"] ?? "e4.dlq.device-telemetry",
        sp.GetRequiredService<ILogger<ServiceBusDeadLetterService>>()));

// ── Azure Event Hub ───────────────────────────────────────────────────────────

builder.Services.AddSingleton(_ => new BlobContainerClient(
    builder.Configuration["Azure:Storage:ConnectionString"],
    builder.Configuration["Azure:Storage:CheckpointContainer"] ?? "event-hub-checkpoints"));

builder.Services.AddSingleton(sp =>
    new EventProcessorClient(
        sp.GetRequiredService<BlobContainerClient>(),
        builder.Configuration["Azure:EventHub:ConsumerGroup"] ?? EventHubConsumerClient.DefaultConsumerGroupName,
        builder.Configuration["Azure:EventHub:ConnectionString"],
        builder.Configuration["Azure:EventHub:Name"] ?? "e4-device-telemetry"));

// ── Domain Services ───────────────────────────────────────────────────────────

builder.Services.AddSingleton<IGeofenceEvaluationService, GeofenceEvaluationService>();
builder.Services.AddSingleton<IDwellTimeCalculationService, DwellTimeCalculationService>();
builder.Services.AddSingleton<IExcursionTimeCalculationService, ExcursionTimeCalculationService>();
builder.Services.AddSingleton<TelemetryEnrichmentOrchestrator>();

// ── SqlBatch Options ──────────────────────────────────────────────────────────

builder.Services.Configure<SqlBatchOptions>(builder.Configuration.GetSection("SqlBatch"));

// ── Pipeline Workers (order matters — warm-up must start before telemetry) ───

builder.Services.AddHostedService<CacheWarmupHostedService>();
builder.Services.AddHostedService<CacheRefreshHostedService>();
builder.Services.AddHostedService<DeviceTelemetryWorker>();
builder.Services.AddHostedService<MessageSegmentationWorker>();
builder.Services.AddHostedService<BusinessEnrichmentWorker>();
builder.Services.AddHostedService<SqlBatchWriterService>();

// ── Health Checks ─────────────────────────────────────────────────────────────

builder.Services.AddHealthChecks()
    .AddSqlServer(sqlConnectionString, name: "sql-server", tags: ["db"])
    .AddRedis(builder.Configuration["Redis:ConnectionString"]!, name: "redis", tags: ["cache"])
    .Add(new HealthCheckRegistration(
        "raw-channel",
        sp => new ChannelHealthCheck<RawTelemetryMessage>(rawChannel, RawChannelCapacity, "raw"),
        HealthStatus.Degraded,
        ["pipeline"]))
    .Add(new HealthCheckRegistration(
        "enriched-channel",
        sp => new ChannelHealthCheck<EnrichedTelemetryEvent>(enrichedChannel, EnrichedChannelCapacity, "enriched"),
        HealthStatus.Degraded,
        ["pipeline"]));

// ── Build + Run ───────────────────────────────────────────────────────────────

var host = builder.Build();
await host.RunAsync();

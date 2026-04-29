// Program.cs — Azure Functions v4 Isolated Worker (.NET 8)
// Replaces the in-process FunctionsStartup pattern with a standard HostBuilder.
using e4scoreDataIngestionFunctionApp.DataAccess;
using e4scoreDataIngestionFunctionApp.Helpers;
using e4scoreDataIngestionFunctionApp.Interfaces;
using e4scoreDataIngestionFunctionApp.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(services =>
    {
        // ── Background services (run at startup, before trigger processing) ───
        // CacheWarmupHostedService pre-loads active device reference data from
        // SQL Server into Redis so the enrichment pipeline has zero SQL lookups
        // for IMEI resolution (Architecture Proposal §D, Layer 1).
        services.AddHostedService<CacheWarmupHostedService>();

        // ── Data access ──────────────────────────────────────────────────────
        services.AddScoped<IMySQLDatabase, MySQLDatabase>();
        services.AddScoped<IAzureRedisCache, AzureRedisCache>();

        // ── Business services ────────────────────────────────────────────────
        services.AddScoped<IProcessDeviceInfo, ProcessDeviceInfo>();
        services.AddScoped<IReverseGeoCoding, ReverseGeoCoding>();
        services.AddScoped<ICalculateDwellTime, CalculateDwellTime>();
        services.AddScoped<ICalculateExcursionTime, CalculateExcursionTime>();

        // ── Queue senders (Singleton — each holds a persistent ServiceBusClient) ─
        services.AddSingleton<IBusinessEnrichmentQueueSender, BusinessEnrichmentQueueSender>();
        services.AddSingleton<IMessageSegmentationQueueSender, MessageSegmentationQueueSender>();
        services.AddSingleton<IEaiQueueSender, EaiQueueSender>();
        services.AddSingleton<IDeviceTelemetryRebuildSender, DeviceTelemetryRebuildSender>();
    })
    .Build();

await host.RunAsync();

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
        // Data access
        services.AddScoped<IMySQLDatabase, MySQLDatabase>();
        services.AddScoped<IAzureRedisCache, AzureRedisCache>();

        // Business services
        services.AddScoped<IProcessDeviceInfo, ProcessDeviceInfo>();
        services.AddScoped<IReverseGeoCoding, ReverseGeoCoding>();
        services.AddScoped<ICalculateDwellTime, CalculateDwellTime>();
        services.AddScoped<ICalculateExcursionTime, CalculateExcursionTime>();

        // Messaging / queue producers
        services.AddScoped<IDeviceProcessingQueue, DeviceProcessingQueue>();
        services.AddScoped<IMessageSegmentationQueue, MessageSegmentationQueue>();
        services.AddScoped<IE4EAIQueue, E4EAIQueue>();
        services.AddScoped<IRebuildQueue, RebuildQueue>();
    })
    .Build();

await host.RunAsync();

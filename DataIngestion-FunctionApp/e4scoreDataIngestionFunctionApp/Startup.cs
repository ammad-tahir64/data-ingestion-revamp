using e4scoreDataIngestionFunctionApp.DataAccess;
using e4scoreDataIngestionFunctionApp.Helpers;
using e4scoreDataIngestionFunctionApp.Interfaces;
using e4scoreDataIngestionFunctionApp.Models;
using e4scoreDataIngestionFunctionApp.Services;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;

[assembly: FunctionsStartup(typeof(FunctionApp.Startup))]

namespace FunctionApp
{
    internal class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            ConfigureServices(builder.Services);
            builder.Services.AddDbContext<ezcheckinContext>();

        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IProcessDeviceInfo, ProcessDeviceInfo>();
            services.AddScoped<IMySQLDatabase, MySQLDatabase>();
            services.AddScoped<IAzureRedisCache, AzureRedisCache>();
            services.AddScoped<IReverseGeoCoding, ReverseGeoCoding>();
            services.AddScoped<ICalculateDwellTime, CalculateDwellTime>();
            services.AddScoped<ICalculateExcursionTime, CalculateExcursionTime>();
            services.AddScoped<IDeviceProcessingQueue, DeviceProcessingQueue>();
            services.AddScoped<IMessageSegmentationQueue, MessageSegmentationQueue>();
            services.AddScoped<IE4EAIQueue, E4EAIQueue>();
            services.AddScoped<IRebuildQueue, RebuildQueue>();
        }
    }
}

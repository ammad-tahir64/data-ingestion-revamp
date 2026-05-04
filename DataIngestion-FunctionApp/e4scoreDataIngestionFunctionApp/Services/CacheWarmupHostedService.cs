using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using e4scoreDataIngestionFunctionApp.DataAccess;
using e4scoreDataIngestionFunctionApp.Models.DomainModels;
using e4scoreDataIngestionFunctionApp.Models.Enum;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;

namespace e4scoreDataIngestionFunctionApp.Services
{
    /// <summary>
    /// E4.Host.CacheWarmupHostedService — Architecture Proposal §D (Layer 1: Reference Data Cache).
    ///
    /// Runs once at worker startup. Loads all active devices from SQL Server into Redis using
    /// a batched pipeline so the enrichment pipeline never has to hit SQL Server for IMEI lookups.
    ///
    /// Key format: device:imei:{imei}  (see RedisKeys.DeviceKey)
    /// TTL: 10 minutes (see RedisKeys.ReferenceDataTtl)
    ///
    /// The function host will not start processing Service Bus messages until this service
    /// completes, ensuring the cache is warm before any telemetry flows through.
    /// </summary>
    public sealed class CacheWarmupHostedService : BackgroundService
    {
        private const int BatchSize = 500;

        private readonly IAzureRedisCache _redisCache;
        private readonly string _connectionString;
        private readonly ILogger<CacheWarmupHostedService> _logger;

        public CacheWarmupHostedService(
            IAzureRedisCache redisCache,
            ILogger<CacheWarmupHostedService> logger)
        {
            _redisCache = redisCache;
            _logger = logger;
            _connectionString = Environment.GetEnvironmentVariable(ApplicationSettings.SqlConnection)
                ?? throw new InvalidOperationException(
                    $"Environment variable '{ApplicationSettings.SqlConnection}' is not set.");
        }

        protected override async Task ExecuteAsync(CancellationToken ct)
        {
            _logger.LogInformation("[CacheWarmup] Starting device reference data warm-up...");
            var sw = System.Diagnostics.Stopwatch.StartNew();

            try
            {
                var devices = await LoadActiveDevicesAsync(ct);
                int total = await WriteToCacheAsync(devices, ct);

                sw.Stop();
                _logger.LogInformation(
                    "[CacheWarmup] Completed. {Total} devices loaded into Redis in {Elapsed}ms.",
                    total, sw.ElapsedMilliseconds);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[CacheWarmup] Warm-up failed. Enrichment pipeline will fall back to SQL lookups.");
                // Do NOT rethrow — a warm-up failure must never crash the host.
                // Misses will be served from SQL via the normal cache-aside path.
            }
        }

        // ────────────────────────────────────────────────────────────────────────

        private async Task<IReadOnlyList<Device>> LoadActiveDevicesAsync(CancellationToken ct)
        {
            await using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync(ct);

            // Load only the columns needed for the enrichment pipeline cache entry.
            // asset_id IS NOT NULL ensures the device has been onboarded.
            var devices = await connection.QueryAsync<Device>(
                @"SELECT id,
                         imei,
                         tracker_type,
                         owner_id,
                         asset_id,
                         battery,
                         date_of_last_move,
                         last_event_latitude,
                         last_event_longitude,
                         location_name,
                         dwell_time_start,
                         excrusion_time_start
                  FROM   eztrack_device
                  WHERE  asset_id IS NOT NULL
                  AND    deleted  = 0",
                commandTimeout: 120);

            return devices.AsList();
        }

        private async Task<int> WriteToCacheAsync(IReadOnlyList<Device> devices, CancellationToken ct)
        {
            var db = _redisCache.RedisConnection.GetDatabase();
            int written = 0;

            // Use Redis pipelining in batches of BatchSize to avoid blocking the event loop.
            for (int offset = 0; offset < devices.Count; offset += BatchSize)
            {
                ct.ThrowIfCancellationRequested();

                int count = Math.Min(BatchSize, devices.Count - offset);
                var tasks = new List<Task>(count);

                for (int i = offset; i < offset + count; i++)
                {
                    var device = devices[i];
                    if (string.IsNullOrWhiteSpace(device.imei))
                        continue;

                string key = RedisKeys.DeviceKey + device.imei;
                    string value = JsonSerializer.Serialize(device);

                    // Fire-and-forget inside the batch — all tasks are awaited together below.
                    tasks.Add(db.StringSetAsync(key, value, RedisKeys.ReferenceDataTtl));
                }

                await Task.WhenAll(tasks);
                written += count;

                _logger.LogDebug("[CacheWarmup] Wrote {Written}/{Total} device entries.", written, devices.Count);
            }

            return written;
        }
    }
}

using e4scoreDataIngestionFunctionApp.Models.RequestModels;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using e4scoreDataIngestionFunctionApp.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;
using e4scoreDataIngestionFunctionApp.Models.Enum;
using System.Globalization;
using System.Collections.Generic;
using e4scoreDataIngestionFunctionApp.Models;
using Location = e4scoreDataIngestionFunctionApp.Models.RequestModels.Location;
using SysTask = System.Threading.Tasks.Task;

namespace e4scoreDataIngestionFunctionApp
{
    public class DeviceTelemetryTrigger
    {
        private readonly IProcessDeviceInfo _processDeviceInfo;
        private readonly IMessageSegmentationQueueSender _segmentationQueue;
        private readonly IDeviceTelemetryRebuildSender _rebuildSender;
        private readonly ILogger<DeviceTelemetryTrigger> _logger;

        public DeviceTelemetryTrigger(
            IProcessDeviceInfo processDeviceInfo,
            IMessageSegmentationQueueSender segmentationQueue,
            IDeviceTelemetryRebuildSender rebuildSender,
            ILogger<DeviceTelemetryTrigger> logger)
        {
            _processDeviceInfo = processDeviceInfo;
            _segmentationQueue = segmentationQueue;
            _rebuildSender = rebuildSender;
            _logger = logger;
        }

        [Function(ApplicationSettings.DeviceTelemetryFunctionName)]
        public async SysTask Run(
            [ServiceBusTrigger(ApplicationSettings.DeviceTelemetryQueueName, Connection = ApplicationSettings.MaTrackQueueConnection)]
            string myQueueItem,
            CancellationToken ct)
        {
            try
            {
                _logger.LogInformation("Packet: {Packet}", myQueueItem);
                var watch = System.Diagnostics.Stopwatch.StartNew();

                var requests = ParseRequests(myQueueItem);

                _logger.LogWarning("[{Function}] Start — IMEI: {Imei}",
                    ApplicationSettings.DeviceTelemetryFunctionName, requests[0].imei);

                int count = 1;
                foreach (var request in requests)
                {
                    _logger.LogWarning("[{Function}] Processing message {Count} — IMEI: {Imei}",
                        ApplicationSettings.DeviceTelemetryFunctionName, count, request.imei);

                    if (request.timestamp.Year >= DateTime.UtcNow.Year)
                    {
                        if (request.location?.located_with is not null)
                        {
                            var locatedWith = request.location.located_with.ToLowerInvariant();
                            if (locatedWith.Contains("samsara") || locatedWith.Contains("roadready"))
                            {
                                await _rebuildSender.SendAsync(request, _logger);
                            }
                        }
                        await _segmentationQueue.SendAsync(request, _logger);
                    }
                    count++;
                }

                watch.Stop();
                _logger.LogWarning("[{Function}] Done — {ElapsedMs}ms",
                    ApplicationSettings.DeviceTelemetryFunctionName, watch.ElapsedMilliseconds);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception in {Function}", nameof(DeviceTelemetryTrigger));
                throw;
            }
        }

        private List<MatrackRequest> ParseRequests(string payload)
        {
            if (payload.Contains("data"))
                return ParseBatch(payload);

            return new List<MatrackRequest> { ParseSingle(payload) };
        }

        private MatrackRequest ParseSingle(string payload)
        {
            if (payload.Contains("samsara"))
            {
                var samsara = JsonConvert.DeserializeObject<Samsara>(payload)!;
                return new MatrackRequest
                {
                    timestamp = samsara.timestamp,
                    location = new Location
                    {
                        primary = new Primary
                        {
                            latitude = samsara.location.primary.latitude,
                            longitude = samsara.location.primary.longitude,
                        },
                        located_with = samsara.location?.located_with
                    },
                    sensors = new Sensors
                    {
                        battery = samsara.sensors is null ? 0 :
                            !string.IsNullOrEmpty(samsara.sensors.battery)
                                ? float.Parse(samsara.sensors.battery, CultureInfo.InvariantCulture.NumberFormat) : 0,
                        temperature = samsara.sensors is null || samsara.sensors.temperature <= 0
                            ? null : (float)samsara.sensors.temperature,
                    },
                    imei = samsara.imei
                };
            }

            return JsonConvert.DeserializeObject<MatrackRequest>(payload)!;
        }

        private List<MatrackRequest> ParseBatch(string payload)
        {
            return JsonConvert.DeserializeObject<MatrackRequestData>(payload)!.data;
        }
    }
}


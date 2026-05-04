using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Threading.Channels;

namespace E4Score.Platform.Worker.HealthChecks;

/// <summary>
/// Reports unhealthy when an in-process channel is more than 80% full.
/// A high channel depth indicates backpressure building up in the pipeline.
/// </summary>
public sealed class ChannelHealthCheck<T> : IHealthCheck
{
    private readonly Channel<T> _channel;
    private readonly int _capacity;
    private readonly string _name;

    public ChannelHealthCheck(Channel<T> channel, int capacity, string name)
    {
        _channel = channel;
        _capacity = capacity;
        _name = name;
    }

    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context,
        CancellationToken ct = default)
    {
        var depth = _channel.Reader.Count;
        var ratio = (double)depth / _capacity;

        var data = new Dictionary<string, object>
        {
            ["depth"] = depth,
            ["capacity"] = _capacity,
            ["fill_ratio"] = $"{ratio:P0}"
        };

        if (ratio >= 0.9)
            return Task.FromResult(HealthCheckResult.Unhealthy($"{_name} channel is {ratio:P0} full", data: data));
        if (ratio >= 0.7)
            return Task.FromResult(HealthCheckResult.Degraded($"{_name} channel is {ratio:P0} full", data: data));

        return Task.FromResult(HealthCheckResult.Healthy(data: data));
    }
}

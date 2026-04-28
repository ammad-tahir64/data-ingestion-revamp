namespace E4Score.Platform.Contracts.Constants;

public static class MetricNames
{
    public const string MessagesReceived = "e4score.messages.received";
    public const string MessagesProcessed = "e4score.messages.processed";
    public const string MessagesFailed = "e4score.messages.failed";
    public const string BatchFlushDurationMs = "e4score.batch.flush_duration_ms";
    public const string BatchSize = "e4score.batch.size";
    public const string CacheHit = "e4score.cache.hit";
    public const string CacheMiss = "e4score.cache.miss";
    public const string ChannelDepthRaw = "e4score.channel.depth.raw";
    public const string ChannelDepthEnriched = "e4score.channel.depth.enriched";
    public const string DeadLetterSent = "e4score.dlq.sent";
}

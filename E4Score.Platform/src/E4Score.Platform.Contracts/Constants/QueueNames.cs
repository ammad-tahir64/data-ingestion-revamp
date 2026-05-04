namespace E4Score.Platform.Contracts.Constants;

public static class QueueNames
{
    // Event Hub
    public const string DeviceTelemetry = "e4-device-telemetry";
    public const string ConsumerGroup = "e4-processing-cg";

    // Service Bus dead-letter queues
    public const string DlqDeviceTelemetry = "e4.dlq.device-telemetry";
    public const string DlqBusinessEnrichment = "e4.dlq.business-enrichment";

    // Service Bus outbox topic
    public const string OutboxTopic = "e4.events.telemetry-processed";
}

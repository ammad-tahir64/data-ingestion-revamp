namespace E4Score.Platform.Contracts.Events;

/// <summary>
/// Deserialized message received from the IoT device via Azure Event Hub.
/// Represents the raw, un-enriched telemetry payload.
/// </summary>
public sealed record RawTelemetryMessage
{
    public string Imei { get; init; } = string.Empty;
    public DateTime Timestamp { get; init; }
    public string? DeviceProvider { get; init; }
    public string? EventType { get; init; }
    public string? TimeZone { get; init; }

    // Location
    public double Latitude { get; init; }
    public double Longitude { get; init; }
    public string? LocatedWith { get; init; }

    // Sensors
    public float? Battery { get; init; }
    public int? Fuel { get; init; }
    public int? Speed { get; init; }
    public int? Mileage { get; init; }
    public string? Direction { get; init; }
    public float? Temperature { get; init; }

    // Event Hub metadata (for checkpointing)
    public string? PartitionId { get; init; }
    public long SequenceNumber { get; init; }
    public string? MessageId { get; init; }
}

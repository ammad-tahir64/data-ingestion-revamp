namespace E4Score.Platform.Contracts.CacheEntries;

/// <summary>
/// Transient device state updated on every telemetry message.
/// Stored in Redis instead of SQL Server to eliminate per-message DB roundtrips.
/// Key: device:state:{imei}
/// </summary>
public sealed class DeviceRuntimeState
{
    public string Imei { get; set; } = string.Empty;
    public double? LastLatitude { get; set; }
    public double? LastLongitude { get; set; }
    public DateTime? LastEventTime { get; set; }
    public DateTime? FirstMoveTime { get; set; }
    public DateTime? LastMoveTime { get; set; }
    public bool IsMoving { get; set; }
    public long? CurrentGeofenceId { get; set; }
    public DateTime? DwellStartTime { get; set; }
    public DateTime? ExcursionStartTime { get; set; }
    public float? LastSpeed { get; set; }
    public DateTime? DateOfLastMove { get; set; }
    public int? MovesInLast30Days { get; set; }
}

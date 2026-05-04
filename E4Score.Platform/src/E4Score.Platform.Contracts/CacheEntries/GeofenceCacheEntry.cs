namespace E4Score.Platform.Contracts.CacheEntries;

/// <summary>
/// Geofence polygon cached in Redis per company.
/// Key: geofence:company:{companyId}
/// </summary>
public sealed class GeofenceCacheEntry
{
    public long LocationId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? Address { get; init; }
    public string? City { get; init; }
    public string? State { get; init; }
    public string? Postal { get; init; }
    public int Zone { get; init; }
    public double CenterLatitude { get; init; }
    public double CenterLongitude { get; init; }
    /// <summary>Radius in meters (used when full polygon is not available).</summary>
    public double RadiusMeters { get; init; }
    /// <summary>WKT polygon geometry for NetTopologySuite evaluation.</summary>
    public string? PolygonWkt { get; init; }
}

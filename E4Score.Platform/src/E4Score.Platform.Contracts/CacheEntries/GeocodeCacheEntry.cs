namespace E4Score.Platform.Contracts.CacheEntries;

/// <summary>
/// Reverse-geocode result cached in Redis.
/// Key: geo:{lat3}:{lng3}   (lat/lng rounded to 3 decimal places ≈ 111m accuracy)
/// </summary>
public sealed class GeocodeCacheEntry
{
    public string? Address { get; init; }
    public string? City { get; init; }
    public string? State { get; init; }
    public string? Postal { get; init; }
    public string? Country { get; init; }
}

using E4Score.Platform.Contracts.CacheEntries;

namespace E4Score.Platform.Contracts.Interfaces;

public interface IReverseGeocodingService
{
    /// <summary>
    /// Returns address details for the given coordinates.
    /// Checks Redis cache first; calls Google Maps API on cache miss.
    /// </summary>
    Task<GeocodeCacheEntry?> GetAddressAsync(double latitude, double longitude, CancellationToken ct = default);
}

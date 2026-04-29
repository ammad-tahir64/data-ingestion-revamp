using E4Score.Platform.Contracts.CacheEntries;
using E4Score.Platform.Contracts.Interfaces;
using E4Score.Platform.Infrastructure.SqlServer.Repositories;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace E4Score.Platform.Infrastructure.Google;

/// <summary>
/// Calls the Google Maps Geocoding API to resolve coordinates to a human-readable address.
/// Wraps RedisCachedGeocodingService — only invoked on cache miss.
/// Successful results are persisted to both Redis (via decorator) and SQL (GeocodeLocationLogs).
/// </summary>
public sealed class GoogleReverseGeocodingService : IReverseGeocodingService
{
    private readonly IReverseGeocodingService _innerCache;
    private readonly GeocodeLocationRepository _geocodeRepo;
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;
    private readonly ILogger<GoogleReverseGeocodingService> _logger;

    public GoogleReverseGeocodingService(
        IReverseGeocodingService innerCache,
        GeocodeLocationRepository geocodeRepo,
        HttpClient httpClient,
        string apiKey,
        ILogger<GoogleReverseGeocodingService> logger)
    {
        _innerCache = innerCache;
        _geocodeRepo = geocodeRepo;
        _httpClient = httpClient;
        _apiKey = apiKey;
        _logger = logger;
    }

    public async Task<GeocodeCacheEntry?> GetAddressAsync(double latitude, double longitude,
        CancellationToken ct = default)
    {
        // 1. Delegate to inner (Redis → SQL) cache first
        var cached = await _innerCache.GetAddressAsync(latitude, longitude, ct);
        if (cached is not null) return cached;

        // 2. Cache miss — call Google Maps Geocoding API
        if (string.IsNullOrEmpty(_apiKey))
        {
            _logger.LogDebug("Google Maps API key not configured — skipping reverse geocode for {Lat},{Lng}",
                latitude, longitude);
            return null;
        }

        try
        {
            var url = $"https://maps.googleapis.com/maps/api/geocode/json?latlng={latitude},{longitude}&key={_apiKey}";
            var response = await _httpClient.GetFromJsonAsync<GoogleGeocodeResponse>(url, ct);

            if (response?.Status != "OK" || response.Results.Count == 0)
            {
                _logger.LogDebug("Google geocode returned {Status} for {Lat},{Lng}", response?.Status, latitude, longitude);
                return null;
            }

            var result = response.Results[0];
            var entry = ParseComponents(result.AddressComponents);

            // 3. Persist to SQL (fire-and-forget, non-critical)
            _ = _geocodeRepo.SaveAsync(latitude, longitude, entry, CancellationToken.None)
                .ContinueWith(t => _logger.LogError(t.Exception, "Failed to persist geocode result"),
                    TaskContinuationOptions.OnlyOnFaulted);

            return entry;
        }
        catch (Exception ex) when (ex is not OperationCanceledException)
        {
            _logger.LogWarning(ex, "Google reverse geocode failed for {Lat},{Lng} — returning null", latitude, longitude);
            return null;
        }
    }

    private static GeocodeCacheEntry ParseComponents(IReadOnlyList<GoogleAddressComponent> components)
    {
        string? streetNumber = null, route = null, city = null, state = null, postal = null, country = null;

        foreach (var c in components)
        {
            if (c.Types.Contains("street_number")) streetNumber = c.ShortName;
            else if (c.Types.Contains("route")) route = c.ShortName;
            else if (c.Types.Contains("locality")) city = c.LongName;
            else if (c.Types.Contains("administrative_area_level_1")) state = c.ShortName;
            else if (c.Types.Contains("postal_code")) postal = c.LongName;
            else if (c.Types.Contains("country")) country = c.ShortName;
        }

        var address = streetNumber is not null && route is not null
            ? $"{streetNumber} {route}"
            : route;

        return new GeocodeCacheEntry
        {
            Address = address,
            City = city,
            State = state,
            Postal = postal,
            Country = country
        };
    }

    // ── Google API response DTOs ─────────────────────────────────────────────

    private sealed class GoogleGeocodeResponse
    {
        [JsonPropertyName("status")]
        public string Status { get; init; } = string.Empty;

        [JsonPropertyName("results")]
        public IReadOnlyList<GoogleGeocodeResult> Results { get; init; } = [];
    }

    private sealed class GoogleGeocodeResult
    {
        [JsonPropertyName("address_components")]
        public IReadOnlyList<GoogleAddressComponent> AddressComponents { get; init; } = [];
    }

    private sealed class GoogleAddressComponent
    {
        [JsonPropertyName("long_name")]
        public string LongName { get; init; } = string.Empty;

        [JsonPropertyName("short_name")]
        public string ShortName { get; init; } = string.Empty;

        [JsonPropertyName("types")]
        public IReadOnlyList<string> Types { get; init; } = [];
    }
}

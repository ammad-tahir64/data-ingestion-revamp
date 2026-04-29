using E4Score.Platform.Contracts.CacheEntries;
using E4Score.Platform.Domain.Services;
using FluentAssertions;
using Xunit;

namespace E4Score.Platform.Tests.Unit.Domain;

public sealed class GeofenceEvaluationServiceTests
{
    private readonly GeofenceEvaluationService _sut = new();

    [Fact]
    public void Evaluate_WhenNoGeofences_ReturnsNull()
    {
        var result = _sut.Evaluate(40.7128, -74.0060, []);
        result.Should().BeNull();
    }

    [Fact]
    public void Evaluate_PointInsideCircularGeofence_ReturnsGeofence()
    {
        var geofences = new List<GeofenceCacheEntry>
        {
            new()
            {
                LocationId = 1,
                Name = "HQ",
                CenterLatitude = 40.7128,
                CenterLongitude = -74.0060,
                RadiusMeters = 500
            }
        };

        // Point is at the center — definitely inside
        var result = _sut.Evaluate(40.7128, -74.0060, geofences);
        result.Should().NotBeNull();
        result!.LocationId.Should().Be(1);
    }

    [Fact]
    public void Evaluate_PointOutsideCircularGeofence_ReturnsNull()
    {
        var geofences = new List<GeofenceCacheEntry>
        {
            new()
            {
                LocationId = 1,
                Name = "HQ",
                CenterLatitude = 40.7128,
                CenterLongitude = -74.0060,
                RadiusMeters = 100 // 100m radius
            }
        };

        // ~1.1km away — outside
        var result = _sut.Evaluate(40.7228, -74.0060, geofences);
        result.Should().BeNull();
    }

    [Fact]
    public void Evaluate_PointInsidePolygonGeofence_ReturnsGeofence()
    {
        // Simple square polygon around 0,0
        const string wkt = "POLYGON((-1 -1, 1 -1, 1 1, -1 1, -1 -1))";
        var geofences = new List<GeofenceCacheEntry>
        {
            new()
            {
                LocationId = 2,
                Name = "Square Zone",
                PolygonWkt = wkt,
                CenterLatitude = 0,
                CenterLongitude = 0,
                RadiusMeters = 0
            }
        };

        var result = _sut.Evaluate(0, 0, geofences);
        result.Should().NotBeNull();
        result!.LocationId.Should().Be(2);
    }

    [Fact]
    public void Evaluate_PointOutsidePolygonGeofence_ReturnsNull()
    {
        const string wkt = "POLYGON((-1 -1, 1 -1, 1 1, -1 1, -1 -1))";
        var geofences = new List<GeofenceCacheEntry>
        {
            new()
            {
                LocationId = 2,
                Name = "Square Zone",
                PolygonWkt = wkt,
                CenterLatitude = 0,
                CenterLongitude = 0,
                RadiusMeters = 0
            }
        };

        var result = _sut.Evaluate(5, 5, geofences);
        result.Should().BeNull();
    }

    [Fact]
    public void Evaluate_MalformedPolygonWkt_FallsBackToRadius()
    {
        var geofences = new List<GeofenceCacheEntry>
        {
            new()
            {
                LocationId = 3,
                Name = "Broken Polygon",
                PolygonWkt = "INVALID WKT",
                CenterLatitude = 40.7128,
                CenterLongitude = -74.0060,
                RadiusMeters = 1000
            }
        };

        // Should fall back to radius check — point is at center
        var result = _sut.Evaluate(40.7128, -74.0060, geofences);
        result.Should().NotBeNull();
    }

    [Fact]
    public void Evaluate_ReturnsFirstMatchingGeofence_WhenMultipleMatch()
    {
        var geofences = new List<GeofenceCacheEntry>
        {
            new()
            {
                LocationId = 10,
                Name = "Outer",
                CenterLatitude = 40.7128,
                CenterLongitude = -74.0060,
                RadiusMeters = 5000
            },
            new()
            {
                LocationId = 11,
                Name = "Inner",
                CenterLatitude = 40.7128,
                CenterLongitude = -74.0060,
                RadiusMeters = 1000
            }
        };

        var result = _sut.Evaluate(40.7128, -74.0060, geofences);
        // Returns the first match in list order
        result!.LocationId.Should().Be(10);
    }
}

using E4Score.Platform.Contracts.CacheEntries;
using E4Score.Platform.Contracts.Interfaces;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;

namespace E4Score.Platform.Domain.Services;

/// <summary>
/// Evaluates whether a device position falls inside any registered geofence polygon.
/// Uses NetTopologySuite for in-memory geometry calculation — no DB calls at evaluation time.
/// </summary>
public sealed class GeofenceEvaluationService : IGeofenceEvaluationService
{
    private static readonly GeometryFactory _geometryFactory = new(new PrecisionModel(), 4326);
    private static readonly WKTReader _wktReader = new(_geometryFactory);

    public GeofenceCacheEntry? Evaluate(double latitude, double longitude,
        IReadOnlyList<GeofenceCacheEntry> geofences)
    {
        if (geofences.Count == 0) return null;

        var devicePoint = _geometryFactory.CreatePoint(new Coordinate(longitude, latitude));

        foreach (var geofence in geofences)
        {
            if (IsInsideGeofence(devicePoint, geofence))
                return geofence;
        }

        return null;
    }

    private static bool IsInsideGeofence(Point devicePoint, GeofenceCacheEntry geofence)
    {
        // Prefer polygon geometry when available
        if (!string.IsNullOrEmpty(geofence.PolygonWkt))
        {
            try
            {
                var polygon = _wktReader.Read(geofence.PolygonWkt);
                return polygon.Contains(devicePoint) || polygon.Intersects(devicePoint);
            }
            catch
            {
                // Fall through to radius check if WKT is malformed
            }
        }

        // Fall back to circular radius check
        if (geofence.RadiusMeters > 0)
        {
            var distanceMeters = HaversineDistanceMeters(
                devicePoint.Y, devicePoint.X,
                geofence.CenterLatitude, geofence.CenterLongitude);
            return distanceMeters <= geofence.RadiusMeters;
        }

        return false;
    }

    private static double HaversineDistanceMeters(double lat1, double lng1, double lat2, double lng2)
    {
        const double earthRadiusM = 6_371_000;
        var dLat = ToRadians(lat2 - lat1);
        var dLng = ToRadians(lng2 - lng1);
        var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2)
              + Math.Cos(ToRadians(lat1)) * Math.Cos(ToRadians(lat2))
              * Math.Sin(dLng / 2) * Math.Sin(dLng / 2);
        var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
        return earthRadiusM * c;
    }

    private static double ToRadians(double degrees) => degrees * Math.PI / 180.0;
}

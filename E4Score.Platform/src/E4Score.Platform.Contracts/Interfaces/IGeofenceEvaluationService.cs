using E4Score.Platform.Contracts.CacheEntries;
using E4Score.Platform.Contracts.Events;

namespace E4Score.Platform.Contracts.Interfaces;

public interface IGeofenceEvaluationService
{
    /// <summary>
    /// Evaluates whether the device position falls inside any geofence polygon
    /// for the given company. All evaluation is in-memory (no DB calls).
    /// </summary>
    GeofenceCacheEntry? Evaluate(double latitude, double longitude, IReadOnlyList<GeofenceCacheEntry> geofences);
}

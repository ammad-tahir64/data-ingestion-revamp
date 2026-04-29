using E4Score.Platform.Contracts.CacheEntries;
using E4Score.Platform.Contracts.Interfaces;

namespace E4Score.Platform.Domain.Services;

/// <summary>
/// Calculates dwell time (time spent stationary at a location) in-memory against
/// the device runtime state from Redis. No DB calls at calculation time.
/// </summary>
public sealed class DwellTimeCalculationService : IDwellTimeCalculationService
{
    public DwellTimeResult Calculate(bool isMove, DateTime sourceTimestamp, DeviceRuntimeState state)
    {
        if (isMove)
        {
            // Device is moving — reset dwell
            return new DwellTimeResult(DwellTimeStart: null, DwellTimeDays: 0);
        }

        // Device is stationary — accumulate dwell time
        var dwellStart = state.DwellStartTime ?? sourceTimestamp;
        var dwellDays = (long)Math.Floor((sourceTimestamp - dwellStart).TotalDays);

        return new DwellTimeResult(dwellStart, dwellDays);
    }
}

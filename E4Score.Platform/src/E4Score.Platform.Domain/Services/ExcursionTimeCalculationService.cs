using E4Score.Platform.Contracts.CacheEntries;
using E4Score.Platform.Contracts.Interfaces;

namespace E4Score.Platform.Domain.Services;

/// <summary>
/// Calculates excursion time (time spent outside the domicile boundary) in-memory.
/// Excursion threshold is 804.67 m (0.5 miles), matching existing business rule.
/// </summary>
public sealed class ExcursionTimeCalculationService : IExcursionTimeCalculationService
{
    private const double ExcursionThresholdMeters = 804.67;

    public ExcursionTimeResult Calculate(bool isMove, DateTime sourceTimestamp,
        DeviceRuntimeState state, double? distanceFromDomicile)
    {
        var isOutsideDomicile = distanceFromDomicile.HasValue
                                && distanceFromDomicile.Value > ExcursionThresholdMeters;

        if (!isOutsideDomicile)
        {
            // Device is back at domicile — reset excursion
            return new ExcursionTimeResult(ExcursionTimeStart: null, ExcursionTimeDays: 0);
        }

        // Device is outside domicile — accumulate excursion time
        var excursionStart = state.ExcursionStartTime ?? sourceTimestamp;
        var excursionDays = (long)Math.Floor((sourceTimestamp - excursionStart).TotalDays);

        return new ExcursionTimeResult(excursionStart, excursionDays);
    }
}

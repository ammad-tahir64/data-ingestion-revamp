using E4Score.Platform.Contracts.CacheEntries;

namespace E4Score.Platform.Contracts.Interfaces;

public record ExcursionTimeResult(DateTime? ExcursionTimeStart, long ExcursionTimeDays);

public interface IExcursionTimeCalculationService
{
    ExcursionTimeResult Calculate(bool isMove, DateTime sourceTimestamp, DeviceRuntimeState state,
        double? distanceFromDomicile);
}

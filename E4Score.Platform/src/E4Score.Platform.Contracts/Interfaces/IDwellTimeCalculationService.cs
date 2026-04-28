using E4Score.Platform.Contracts.CacheEntries;

namespace E4Score.Platform.Contracts.Interfaces;

public record DwellTimeResult(DateTime? DwellTimeStart, long DwellTimeDays);

public interface IDwellTimeCalculationService
{
    DwellTimeResult Calculate(bool isMove, DateTime sourceTimestamp, DeviceRuntimeState state);
}

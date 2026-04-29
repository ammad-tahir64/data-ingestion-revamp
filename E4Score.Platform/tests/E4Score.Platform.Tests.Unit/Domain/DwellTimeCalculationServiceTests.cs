using E4Score.Platform.Contracts.CacheEntries;
using E4Score.Platform.Domain.Services;
using FluentAssertions;
using Xunit;

namespace E4Score.Platform.Tests.Unit.Domain;

public sealed class DwellTimeCalculationServiceTests
{
    private readonly DwellTimeCalculationService _sut = new();

    [Fact]
    public void Calculate_WhenIsMove_ReturnsDwellReset()
    {
        var state = new DeviceRuntimeState { DwellStartTime = DateTime.UtcNow.AddDays(-3) };
        var result = _sut.Calculate(isMove: true, DateTime.UtcNow, state);

        result.DwellTimeStart.Should().BeNull();
        result.DwellTimeDays.Should().Be(0);
    }

    [Fact]
    public void Calculate_WhenStationary_WithNoPriorDwellStart_SetsStartToTimestamp()
    {
        var now = new DateTime(2026, 1, 15, 12, 0, 0, DateTimeKind.Utc);
        var state = new DeviceRuntimeState { DwellStartTime = null };

        var result = _sut.Calculate(isMove: false, now, state);

        result.DwellTimeStart.Should().Be(now);
        result.DwellTimeDays.Should().Be(0);
    }

    [Fact]
    public void Calculate_WhenStationary_AccumulatesDwellDays()
    {
        var start = new DateTime(2026, 1, 10, 12, 0, 0, DateTimeKind.Utc);
        var now = new DateTime(2026, 1, 15, 12, 0, 0, DateTimeKind.Utc); // 5 days later
        var state = new DeviceRuntimeState { DwellStartTime = start };

        var result = _sut.Calculate(isMove: false, now, state);

        result.DwellTimeStart.Should().Be(start);
        result.DwellTimeDays.Should().Be(5);
    }

    [Fact]
    public void Calculate_WhenStationary_PartialDayFlooredToWholeDays()
    {
        var start = new DateTime(2026, 1, 10, 6, 0, 0, DateTimeKind.Utc);
        var now = new DateTime(2026, 1, 12, 18, 0, 0, DateTimeKind.Utc); // 2.5 days later
        var state = new DeviceRuntimeState { DwellStartTime = start };

        var result = _sut.Calculate(isMove: false, now, state);

        result.DwellTimeDays.Should().Be(2); // Floor(2.5) = 2
    }
}

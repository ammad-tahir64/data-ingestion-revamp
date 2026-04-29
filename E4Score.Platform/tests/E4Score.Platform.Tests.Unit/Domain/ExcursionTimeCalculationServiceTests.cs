using E4Score.Platform.Contracts.CacheEntries;
using E4Score.Platform.Domain.Services;
using FluentAssertions;
using Xunit;

namespace E4Score.Platform.Tests.Unit.Domain;

public sealed class ExcursionTimeCalculationServiceTests
{
    private readonly ExcursionTimeCalculationService _sut = new();

    // The excursion threshold is 804.67 m (0.5 miles)

    [Fact]
    public void Calculate_WhenWithinDomicileRadius_ReturnsExcursionReset()
    {
        var state = new DeviceRuntimeState { ExcursionStartTime = DateTime.UtcNow.AddDays(-2) };
        var result = _sut.Calculate(isMove: false, DateTime.UtcNow, state, distanceFromDomicile: 500);

        result.ExcursionTimeStart.Should().BeNull();
        result.ExcursionTimeDays.Should().Be(0);
    }

    [Fact]
    public void Calculate_WhenDistanceIsNull_ReturnsExcursionReset()
    {
        var state = new DeviceRuntimeState { ExcursionStartTime = DateTime.UtcNow.AddDays(-2) };
        var result = _sut.Calculate(isMove: true, DateTime.UtcNow, state, distanceFromDomicile: null);

        result.ExcursionTimeStart.Should().BeNull();
        result.ExcursionTimeDays.Should().Be(0);
    }

    [Fact]
    public void Calculate_WhenOutsideDomicile_WithNoPriorStart_SetsStartToTimestamp()
    {
        var now = new DateTime(2026, 1, 20, 10, 0, 0, DateTimeKind.Utc);
        var state = new DeviceRuntimeState { ExcursionStartTime = null };

        var result = _sut.Calculate(isMove: true, now, state, distanceFromDomicile: 1500);

        result.ExcursionTimeStart.Should().Be(now);
        result.ExcursionTimeDays.Should().Be(0);
    }

    [Fact]
    public void Calculate_WhenOutsideDomicile_AccumulatesExcursionDays()
    {
        var start = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        var now = new DateTime(2026, 1, 8, 0, 0, 0, DateTimeKind.Utc); // 7 days later
        var state = new DeviceRuntimeState { ExcursionStartTime = start };

        var result = _sut.Calculate(isMove: false, now, state, distanceFromDomicile: 2000);

        result.ExcursionTimeStart.Should().Be(start);
        result.ExcursionTimeDays.Should().Be(7);
    }

    [Fact]
    public void Calculate_WhenExactlyAtThreshold_IsNotExcursion()
    {
        // 804.67 meters is the threshold — exactly at threshold is NOT outside
        var state = new DeviceRuntimeState();
        var result = _sut.Calculate(isMove: false, DateTime.UtcNow, state, distanceFromDomicile: 804.67);

        result.ExcursionTimeStart.Should().BeNull();
        result.ExcursionTimeDays.Should().Be(0);
    }

    [Fact]
    public void Calculate_WhenJustOverThreshold_IsExcursion()
    {
        var now = DateTime.UtcNow;
        var state = new DeviceRuntimeState { ExcursionStartTime = null };

        var result = _sut.Calculate(isMove: false, now, state, distanceFromDomicile: 804.68);

        result.ExcursionTimeStart.Should().Be(now);
    }
}

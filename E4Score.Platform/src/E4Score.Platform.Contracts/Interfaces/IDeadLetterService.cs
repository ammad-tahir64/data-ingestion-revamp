using E4Score.Platform.Contracts.Events;

namespace E4Score.Platform.Contracts.Interfaces;

public interface IDeadLetterService
{
    Task SendToDeadLetterAsync(
        RawTelemetryMessage message,
        Exception exception,
        int retryCount,
        CancellationToken ct = default);
}

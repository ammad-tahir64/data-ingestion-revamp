namespace E4Score.Platform.Contracts.Interfaces;

/// <summary>
/// Redis-backed idempotency check. Prevents duplicate processing of resent IoT messages.
/// </summary>
public interface IIdempotencyService
{
    /// <returns>True if the message was claimed (first time seen); false if it is a duplicate.</returns>
    Task<bool> TryClaimAsync(string messageId, TimeSpan expiry, CancellationToken ct = default);
}

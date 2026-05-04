using E4Score.Platform.Contracts.Constants;
using E4Score.Platform.Contracts.Interfaces;
using StackExchange.Redis;

namespace E4Score.Platform.Infrastructure.Redis;

/// <summary>
/// Redis-backed idempotency check using SETNX (SET if Not eXists).
/// Prevents duplicate processing of IoT messages that are resent due to at-least-once delivery.
/// </summary>
public sealed class RedisIdempotencyService : IIdempotencyService
{
    private readonly IDatabase _db;

    public RedisIdempotencyService(IConnectionMultiplexer redis)
    {
        _db = redis.GetDatabase();
    }

    /// <inheritdoc/>
    public async Task<bool> TryClaimAsync(string messageId, TimeSpan expiry, CancellationToken ct = default)
    {
        var key = CacheKeys.IdempotencyKey(messageId);
        // Returns true only when the key did NOT exist (first time we see this message)
        return await _db.StringSetAsync(
            key,
            DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
            expiry,
            When.NotExists);
    }
}

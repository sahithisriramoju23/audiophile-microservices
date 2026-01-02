using StackExchange.Redis;
using System.Text.Json;

namespace Cart.API.Data;

public class AzureRedisCacheService : ICacheService
{
    private readonly IDatabase _cache;
    private readonly ILogger _logger;

    private const int  _lockTimeoutSeconds = 60;
    private const int  _lockRetryDelaySeconds = 5;
    private const int  _lockMaxRetryAttempts = 3;
    private const string _cacheLockKeySuffix = "{0}:lock";

    public AzureRedisCacheService(IConnectionMultiplexer redisCacheConnection, ILogger<AzureRedisCacheService> logger)
    {
        _cache = redisCacheConnection.GetDatabase();
        _logger = logger;
    }

    public async Task RemoveCacheAsync(string key, CancellationToken cancellationToken = default)
    {
        await _cache.KeyDeleteAsync(key).ConfigureAwait(false);
    }
    public async Task SetCacheAsync<T>(string key, T value, TimeSpan expiry, CancellationToken cancellationToken = default)
    {
        var cacheValue = JsonSerializer.Serialize(value);
        await _cache.StringSetAsync(key, cacheValue, expiry).ConfigureAwait(false);
    }
    //using locking to prevent cache stampede
    //A cache stampede happens when concurrent requests encounter a cache miss and try to fetch the data from the source.
    //This can overload your application and negate the benefits of caching.
    public async Task<T> GetOrSetCacheAsync<T>(string key, Func<Task<T>> valueFunc, TimeSpan expiry)
    {
        var cachedData = await _cache.StringGetAsync(key).ConfigureAwait(false);
        if (cachedData.HasValue)
        {
            return JsonSerializer.Deserialize<T>(cachedData)!;
        }

        //The data is not in cache, acquire it and set the cache
        var lockKey = string.Format(_cacheLockKeySuffix, key);
        var lockToken = Guid.NewGuid().ToString();
        var lockTimeout = TimeSpan.FromSeconds(_lockTimeoutSeconds);
        var retryDelay = TimeSpan.FromSeconds(_lockRetryDelaySeconds);

        //Try to acquire value and set cache value with distributed lock
        //retry for 3 times 
        for(int attempt = 1; attempt <= _lockMaxRetryAttempts; attempt++)
        {
            //try to acquire the lock
            var lockAcquired = await _cache.StringSetAsync(lockKey, lockToken, lockTimeout, When.NotExists).ConfigureAwait(false);
            if (lockAcquired)
            {
                try
                {
                    var  value = await valueFunc().ConfigureAwait(false);
                    var cacheValue = JsonSerializer.Serialize(value);
                    await _cache.StringSetAsync(key, cacheValue,expiry).ConfigureAwait(false);
                    _logger.LogInformation($"{attempt} Attempt for {lockToken}: Acquired lock and set cache for key: {key}");
                    return value;
                }catch(HttpRequestException ex)
                {
                    _logger.LogError(ex, $"{attempt} Attempt for {lockToken}: Error acquiring value for key: {key}");
                }
                catch(RedisException ex)
                {
                    _logger.LogError(ex, $"{attempt} Attempt for {lockToken}: Error acquiring value for key: {key}");
                }
                catch(Exception ex)
                {
                    _logger.LogError(ex, $"{attempt} Attempt for {lockToken}: Unexpected error acquiring value for key: {key}");
                }
                finally
                {
                    //Release the lock
                    var currentLockToken = await _cache.StringGetAsync(lockKey).ConfigureAwait(false);
                    if(currentLockToken == lockToken)
                    {
                        await _cache.KeyDeleteAsync(lockKey).ConfigureAwait(false);
                        _logger.LogInformation($"{attempt} Attempt for {lockToken}: Released lock for key: {key}");
                    }
                }
            }
            await Task.Delay(retryDelay).ConfigureAwait(false);

            //check if cache was populated while waiting for the lock
            cachedData = await _cache.StringGetAsync(key).ConfigureAwait(false);
            if (cachedData.HasValue)
            {
                _logger.LogInformation($"{attempt} Attempt for {lockToken}: Cache populated while waiting for lock for key: {key}");
                return JsonSerializer.Deserialize<T>(cachedData)!;
            }
        }
        //Final attempt to get the value without caching
        return await valueFunc().ConfigureAwait(false);
    }
}

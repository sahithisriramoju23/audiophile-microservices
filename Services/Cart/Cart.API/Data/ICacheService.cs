namespace Cart.API.Data;

public interface ICacheService
{
    Task<T> GetOrSetCacheAsync<T>(string key, Func<Task<T>> valueFunc, TimeSpan expiry);
    Task RemoveCacheAsync(string key, CancellationToken cancellationToken = default);
    Task SetCacheAsync<T>(string key, T value, TimeSpan expiry, CancellationToken cancellationToken = default);
}

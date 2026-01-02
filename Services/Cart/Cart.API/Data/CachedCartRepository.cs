using Cart.API.Models;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;

namespace Cart.API.Data;

public class CachedCartRepository(ICartRepository repository, ICacheService cache) : ICartRepository
{
    private static readonly TimeSpan TTlInMinutes = TimeSpan.FromMinutes(30); 
    public async Task<bool> DeleteCart(string userName, CancellationToken cancellationToken = default)
    {
        await cache.RemoveCacheAsync(userName, cancellationToken);
        return await repository.DeleteCart(userName, cancellationToken);
    }

    public async Task<ShoppingCart> GetCart(string userName, CancellationToken cancellationToken = default)
    {
        var cachedBasket = await cache.GetOrSetCacheAsync(userName, 
            () => repository.GetCart(userName, cancellationToken), 
            TTlInMinutes);
        return cachedBasket;

        /*var cachedBasket = await cache.Get(userName, cancellationToken);
        if (!string.IsNullOrEmpty(cachedBasket))
        {
            return JsonSerializer.Deserialize<ShoppingCart>(cachedBasket)!;
        }
        var basket = await repository.GetCart(userName, cancellationToken);
        await cache.SetStringAsync(userName, JsonSerializer.Serialize(basket), cancellationToken);
        return basket;*/
    }

    public async Task<ShoppingCart> StoreCart(ShoppingCart basket, CancellationToken cancellationToken = default)
    {
        await cache.SetCacheAsync(basket.UserName, basket, TTlInMinutes, cancellationToken);
        return await repository.StoreCart(basket, cancellationToken);
    }
}

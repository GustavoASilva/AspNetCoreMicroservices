using Basket.API.Entities;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Basket.API.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDistributedCache _redisCache;

        public BasketRepository(IDistributedCache redisCache)
        {
            _redisCache = redisCache;
        }

        public async Task DeleteBasketAsync(string userName)
        {
            await _redisCache.RemoveAsync(userName);
        }

        public async Task<ShoppingCart> GetBasketAsync(string userName)
        {
            var shoppingCartJson = await _redisCache.GetStringAsync(userName);
            if (string.IsNullOrEmpty(shoppingCartJson))
            {
                return null;
            }

            return JsonSerializer.Deserialize<ShoppingCart>(shoppingCartJson);
        }

        public async Task<ShoppingCart> UpdateBasketAsync(ShoppingCart basket)
        {
            var basketJson = JsonSerializer.Serialize(basket);
            await _redisCache.SetStringAsync(basket.UserName, basketJson, new DistributedCacheEntryOptions() { SlidingExpiration = TimeSpan.FromMinutes(30)});
            return await GetBasketAsync(basket.UserName);
        }
    }
}

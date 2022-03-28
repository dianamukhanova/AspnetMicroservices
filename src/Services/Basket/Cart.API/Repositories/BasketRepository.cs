using System;
using Cart.API.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

//redis
// key: username
// value: basket 

namespace Cart.API.Repositories
{
	public class BasketRepository: IBasketRepository
	{
        private readonly IDistributedCache _redisCache;
		public BasketRepository(IDistributedCache redisCache)
		{
            _redisCache = redisCache;
		}

        public async Task DeleteBasket(string username)
        {
            await _redisCache.RemoveAsync(username);
        }

        public async Task<ShoppingCart> GetBasket(string username)
        {
            var basket = await _redisCache.GetStringAsync(username);
            if (string.IsNullOrEmpty(basket))
                return null;
            // basket is in json
            return JsonConvert.DeserializeObject<ShoppingCart>(basket);
        }

        public async Task<ShoppingCart> UpdateBasket(ShoppingCart basket)
        {
            await _redisCache.SetStringAsync(basket.UserName, JsonConvert.SerializeObject(basket));
            return await GetBasket(basket.UserName);
        }
    }
}


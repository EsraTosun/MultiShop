using MultiShop.Basket.Dtos;
using MultiShop.Basket.Settings;
using StackExchange.Redis;
using System.Text.Json;

namespace MultiShop.Basket.Services
{
    public class BasketService : IBasketService
    {
        private readonly IDatabase _database;

        public BasketService(RedisService redisService)
        {
            _database = redisService.GetDb();
        }

        private string GetBasketKey(string userId) => $"basket:{userId}";

        public async Task<bool> DeleteBasket(string userId)
        {
            return await _database.KeyDeleteAsync(GetBasketKey(userId));
        }

        public async Task<BasketTotalDto?> GetBasket(string userId)
        {
            var basket = await _database.StringGetAsync(GetBasketKey(userId));

            if (basket.IsNullOrEmpty)
                return null;

            return JsonSerializer.Deserialize<BasketTotalDto>(basket);
        }

        public async Task SaveBasket(BasketTotalDto basketTotalDto)
        {
            await _database.StringSetAsync(
                GetBasketKey(basketTotalDto.UserId),
                JsonSerializer.Serialize(basketTotalDto),
                TimeSpan.FromMinutes(30)
            );
        }
    }
}

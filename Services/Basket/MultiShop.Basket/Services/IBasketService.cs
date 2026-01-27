using MultiShop.Basket.Dtos;
using StackExchange.Redis;

namespace MultiShop.Basket.Services
{
    public interface IBasketService
    {
        Task<BasketTotalDto> GetBasket(string userId);
        Task SaveBasket(BasketTotalDto basketTotalDto);
        Task<bool> DeleteBasket(string userId);
    }
}

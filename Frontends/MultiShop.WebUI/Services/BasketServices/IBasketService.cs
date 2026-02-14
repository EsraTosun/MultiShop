using MultiShop.DtoLayer.BasketDtos;

namespace MultiShop.WebUI.Services.BasketServices
{
    public interface IBasketService
    {
        Task<BasketTotalDto> GetBasket();
        Task<bool> SaveBasket(BasketTotalDto basketTotalDto);
        Task DeleteBasket(string userId);
        Task<bool> AddBasketItem(BasketItemDto basketItemDto);
        Task<bool> UpdateBasketItemQuantity(string productId, int quantity);
        Task<bool> RemoveBasketItem(string productId);
    }
}

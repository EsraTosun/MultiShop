using MultiShop.DtoLayer.BasketDtos;
using System.Net;
using System.Security.Claims;

namespace MultiShop.WebUI.Services.BasketServices
{
    public class BasketService : IBasketService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BasketService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> AddBasketItem(BasketItemDto basketItemDto)
        {
            var basket = await GetBasket();

            if (basket == null)
            {
                basket = new BasketTotalDto
                {
                    UserId = GetUserId(),
                    DiscountCode = "",
                    BasketItems = new List<BasketItemDto>()
                };
            }

            var existingItem = basket.BasketItems
                .FirstOrDefault(x => x.ProductId == basketItemDto.ProductId);

            if (existingItem == null)
            {
                basket.BasketItems.Add(basketItemDto);
            }
            else
            {
                existingItem.Quantity += 1;
            }

            return await SaveBasket(basket);
        }

        public Task DeleteBasket(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<BasketTotalDto?> GetBasket()
        {
            var response = await _httpClient.GetAsync("baskets");

            if (response.StatusCode == HttpStatusCode.NoContent)
                return null;

            if (!response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadFromJsonAsync<BasketTotalDto>();
        }

        public async Task<bool> RemoveBasketItem(string productId)
        {
            var basket = await GetBasket();

            var deletedItem = basket.BasketItems
                .FirstOrDefault(x => x.ProductId == productId);

            if (deletedItem == null)
                return false;

            basket.BasketItems.Remove(deletedItem);

            return await SaveBasket(basket);
        }

        public async Task<bool> SaveBasket(BasketTotalDto basketTotalDto) 
        { 
            var response = await _httpClient.PostAsJsonAsync("baskets", basketTotalDto); 
            
            return response.StatusCode == HttpStatusCode.OK || 
                response.StatusCode == HttpStatusCode.Created || 
                response.StatusCode == HttpStatusCode.NoContent; 
        }

        private string GetUserId()
        {
            return _httpContextAccessor.HttpContext?
                .User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                ?? "guest-user"; 
        }
    }
}

using MultiShop.DtoLayer.BasketDtos;

namespace MultiShop.WebUI.Services.BasketServices
{
    public class BasketService : IBasketService
    {
        private readonly HttpClient _httpClient;
        public BasketService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<bool> AddBasketItem(BasketItemDto basketItemDto)
        {
            var values = await GetBasket();

            if (values == null)
            {
                values = new BasketTotalDto
                {
                    BasketItems = new List<BasketItemDto>()
                };
            }

            var existingItem = values.BasketItems
                .FirstOrDefault(x => x.ProductId == basketItemDto.ProductId);

            if (existingItem == null)
            {
                values.BasketItems.Add(basketItemDto);
            }
            else
            {
                existingItem.Quantity += 1;
            }

            return await SaveBasket(values);
        }

        public Task DeleteBasket(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<BasketTotalDto> GetBasket()
        {
            var response = await _httpClient.GetAsync("baskets");

            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                return new BasketTotalDto
                {
                    BasketItems = new List<BasketItemDto>()
                };
            }

            return await response.Content.ReadFromJsonAsync<BasketTotalDto>();
        }

        public async Task<bool> RemoveBasketItem(string productId)
        {
            var values = await GetBasket();
            var deletedItem = values.BasketItems.FirstOrDefault(x => x.ProductId == productId);
            var result=values.BasketItems.Remove(deletedItem);
            await SaveBasket(values);
            return true;
        }

        public async Task<bool> SaveBasket(BasketTotalDto basketTotalDto)
        {
            var response = await _httpClient.PostAsJsonAsync("baskets", basketTotalDto);

            return response.IsSuccessStatusCode;
        }
    }
}

using MultiShop.DtoLayer.DiscountDtos;

namespace MultiShop.WebUI.Services.DiscountServices
{
    public class DiscountService : IDiscountService
    {
        private readonly HttpClient _httpClient;
        public DiscountService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<GetDiscountCodeDetailByCode> GetDiscountCode(string code)
        {
            var responseMessage = await _httpClient.GetAsync(
                "discounts/GetCodeDetailByCodeAsync?code=" + code
            );

            if (!responseMessage.IsSuccessStatusCode)
                return null;

            return await responseMessage.Content
                .ReadFromJsonAsync<GetDiscountCodeDetailByCode>();
        }


        public async Task<int> GetDiscountCouponCountRate(string code)
        {
            var responseMessage = await _httpClient.GetAsync(
                "discounts/GetDiscountCouponRateByCodeAsync?code=" + code
            );

            if (!responseMessage.IsSuccessStatusCode)
                return 0;

            var rate = await responseMessage.Content.ReadFromJsonAsync<int>();
            return rate;
        }
    }
}


using MultiShop.WebUI.Models;

namespace MultiShop.WebUI.Services.StatisticServices.DiscountStatisticServices
{
    public class DiscountStatisticService : IDiscountStatisticService
    {
        private readonly HttpClient _httpClient;
        public DiscountStatisticService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<int> GetDiscountCouponCount()
        {
            var responseMessage = await _httpClient.GetAsync("Discounts/GetDiscountCouponCount");

            var response = await responseMessage
                .Content
                .ReadFromJsonAsync<DiscountCouponCountDto>();

            return response!.DiscountCouponCount;
        }
    }
}

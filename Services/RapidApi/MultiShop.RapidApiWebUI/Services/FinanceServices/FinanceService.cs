using Microsoft.Extensions.Options;
using MultiShop.RapidApiWebUI.Models;
using System.Text.Json;

namespace MultiShop.RapidApiWebUI.Services.FinanceServices
{
    public class FinanceService : IFinanceService
    {
        private readonly HttpClient _httpClient;
        private readonly FinanceApiOptions _options;

        public FinanceService(HttpClient httpClient, IOptions<FinanceApiOptions> options)
        {
            _httpClient = httpClient;
            _options = options.Value;
        }

        public async Task<CurrencyViewModel> GetExchangeRateAsync(string from, string to)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{_options.BaseUrl}currency-exchange-rate?from_symbol={from}&to_symbol={to}&language=en")
            };

            request.Headers.Add("x-rapidapi-key", _options.ApiKey);
            request.Headers.Add("x-rapidapi-host", _options.Host);

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            using var doc = JsonDocument.Parse(json);

            var root = doc.RootElement;
            var data = root.GetProperty("data");

            return new CurrencyViewModel
            {
                Status = root.GetProperty("status").GetString(),
                RequestId = root.GetProperty("request_id").GetString(),
                FromSymbol = data.GetProperty("from_symbol").GetString(),
                ToSymbol = data.GetProperty("to_symbol").GetString(),
                ExchangeRate = data.GetProperty("exchange_rate").GetDecimal(),
                PreviousClose = data.GetProperty("previous_close").GetDecimal(),
                LastUpdateUtc = DateTime.Parse(data.GetProperty("last_update_utc").GetString())
            };
        }
    }
}

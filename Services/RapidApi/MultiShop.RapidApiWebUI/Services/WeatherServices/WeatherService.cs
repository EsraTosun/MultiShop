using Microsoft.Extensions.Options;
using System.Net.Http.Headers;

namespace MultiShop.RapidApiWebUI.Services.WeatherServices
{
    public class WeatherService : IWeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly WeatherApiOptions _options;

        public WeatherService(HttpClient httpClient, IOptions<WeatherApiOptions> options)
        {
            _httpClient = httpClient;
            _options = options.Value;
        }

        public async Task<string> GetCurrentWeatherAsync(string city, string countryCode)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(
                    $"https://{_options.Host}/api/weather/current?place={city},{countryCode}&units=standard&lang=en&mode=json"),
            };

            request.Headers.Add("x-rapidapi-key", _options.ApiKey);
            request.Headers.Add("x-rapidapi-host", _options.Host);

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
    }
}


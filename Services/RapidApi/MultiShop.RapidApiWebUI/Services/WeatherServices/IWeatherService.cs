namespace MultiShop.RapidApiWebUI.Services.WeatherServices
{
    public interface IWeatherService
    {
        Task<string> GetCurrentWeatherAsync(string city, string countryCode);
    }
}

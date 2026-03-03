using Microsoft.AspNetCore.Mvc;
using MultiShop.RapidApiWebUI.Models;
using MultiShop.RapidApiWebUI.Services.WeatherServices;
using System.Text.Json;

namespace MultiShop.RapidApiWebUI.Controllers
{
    public class WeatherController : Controller
    {
        private readonly IWeatherService _weatherService;

        public WeatherController(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        public async Task<IActionResult> Index(string city = "London", string countryCode = "GB")
        {
            // 1️⃣ API’den JSON al
            var weatherJson = await _weatherService.GetCurrentWeatherAsync(city, countryCode);

            // 2️⃣ JSON parse
            var jsonDoc = JsonDocument.Parse(weatherJson);
            var root = jsonDoc.RootElement;

            // 3️⃣ ViewModel’e map et
            var model = new WeatherViewModel
            {
                City = root.GetProperty("name").GetString(),
                Country = root.GetProperty("sys").GetProperty("country").GetString(),
                Summary = root.GetProperty("summery").GetString(),

                WeatherMain = root.GetProperty("weather")[0].GetProperty("main").GetString(),
                WeatherDescription = root.GetProperty("weather")[0].GetProperty("description").GetString(),
                WeatherIcon = root.GetProperty("weather")[0].GetProperty("icon").GetString(),

                Temperature = root.GetProperty("main").GetProperty("temprature").GetDouble(),
                TemperatureFeelsLike = root.GetProperty("main").GetProperty("temprature_feels_like").GetDouble(),
                TemperatureMin = root.GetProperty("main").GetProperty("temprature_min").GetDouble(),
                TemperatureMax = root.GetProperty("main").GetProperty("temprature_max").GetDouble(),
                TemperatureUnit = root.GetProperty("main").GetProperty("temprature_unit").GetString(),

                Humidity = root.GetProperty("main").GetProperty("humidity").GetInt32(),
                HumidityUnit = root.GetProperty("main").GetProperty("humidity_unit").GetString(),
                Pressure = root.GetProperty("main").GetProperty("pressure").GetInt32(),
                PressureUnit = root.GetProperty("main").GetProperty("pressure_unit").GetString(),

                WindSpeed = root.GetProperty("wind").GetProperty("speed").GetDouble(),
                WindDegree = root.GetProperty("wind").GetProperty("degrees").GetInt32(),
                WindUnit = root.GetProperty("wind").GetProperty("speed_unit").GetString(),

                Cloudiness = root.GetProperty("clouds").GetProperty("cloudiness").GetInt32(),
                CloudUnit = root.GetProperty("clouds").GetProperty("unit").GetString(),

                RainAmount = root.GetProperty("rain").GetProperty("amount").GetDouble(),
                RainUnit = root.GetProperty("rain").GetProperty("unit").GetString(),
                SnowAmount = root.GetProperty("snow").GetProperty("amount").GetDouble(),
                SnowUnit = root.GetProperty("snow").GetProperty("unit").GetString(),

                Visibility = root.GetProperty("visibility_distance").GetInt32(),
                VisibilityUnit = root.GetProperty("visibility_unit").GetString(),

                DateTime = DateTime.Parse(root.GetProperty("dt_txt").GetString()),
                Sunrise = DateTime.Parse(root.GetProperty("sys").GetProperty("sunrise_txt").GetString()),
                Sunset = DateTime.Parse(root.GetProperty("sys").GetProperty("sunset_txt").GetString()),

                Longitude = root.GetProperty("coord").GetProperty("lon").GetDouble(),
                Latitude = root.GetProperty("coord").GetProperty("lat").GetDouble(),
            };

            // 4️⃣ View’a gönder
            return View(model);
        }
    }
}
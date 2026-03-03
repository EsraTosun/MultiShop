namespace MultiShop.RapidApiWebUI.Models
{
    public class WeatherViewModel
    {
        // Genel
        public string City { get; set; }            // name
        public string Country { get; set; }         // sys.country
        public string Summary { get; set; }         // summery

        // Hava durumu
        public string WeatherMain { get; set; }     // weather[0].main
        public string WeatherDescription { get; set; } // weather[0].description
        public string WeatherIcon { get; set; }     // weather[0].icon

        // Sıcaklıklar
        public double Temperature { get; set; }     // main.temprature
        public double TemperatureFeelsLike { get; set; } // main.temprature_feels_like
        public double TemperatureMin { get; set; }  // main.temprature_min
        public double TemperatureMax { get; set; }  // main.temprature_max
        public string TemperatureUnit { get; set; } // main.temprature_unit

        // Nem ve basınç
        public int Humidity { get; set; }           // main.humidity
        public string HumidityUnit { get; set; }    // main.humidity_unit
        public int Pressure { get; set; }           // main.pressure
        public string PressureUnit { get; set; }    // main.pressure_unit

        // Rüzgar
        public double WindSpeed { get; set; }       // wind.speed
        public int WindDegree { get; set; }         // wind.degrees
        public string WindUnit { get; set; }        // wind.speed_unit

        // Bulutlar
        public int Cloudiness { get; set; }         // clouds.cloudiness
        public string CloudUnit { get; set; }       // clouds.unit

        // Yağış ve kar
        public double RainAmount { get; set; }      // rain.amount
        public string RainUnit { get; set; }        // rain.unit
        public double SnowAmount { get; set; }      // snow.amount
        public string SnowUnit { get; set; }        // snow.unit

        // Görünürlük
        public int Visibility { get; set; }         // visibility_distance
        public string VisibilityUnit { get; set; }  // visibility_unit

        // Tarihler
        public DateTime DateTime { get; set; }      // dt_txt
        public DateTime Sunrise { get; set; }       // sys.sunrise_txt
        public DateTime Sunset { get; set; }        // sys.sunset_txt

        // Koordinatlar
        public double Longitude { get; set; }       // coord.lon
        public double Latitude { get; set; }        // coord.lat
    }
}

using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using OpenWeatherMap_API.Models;

namespace OpenWeatherMap_API.Services
{
    public class OpenWeatherService : IOpenWeatherService
    {
        public readonly string ApiKey;

        public OpenWeatherService(IOptions<OpenWeatherSettings> openWeatherSettings)
        {
            ApiKey = openWeatherSettings.Value.ApiKey;
        }

        private static readonly HttpClient _httpClient = new()
        {
            BaseAddress = new Uri("https://api.openweathermap.org/data/2.5/")
        };

        public async Task<ResponseWeather?> GetCurrentWeatherForCity(string city)
        {
            var response = await _httpClient.GetAsync($"/data/2.5/weather?q={city}&units=metric&appid={ApiKey}");

            if (response.IsSuccessStatusCode)
            {
                string stringResult = await response.Content.ReadAsStringAsync();
                var rawWeather = JsonConvert.DeserializeObject<ResponseWeather>(stringResult);
                return rawWeather;
            }

            return null;
        }
    }
}

using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using OpenWeatherMap_API.Models;

namespace OpenWeatherMap_API.Services
{
    public class OpenWeatherService : IOpenWeatherService
    {
        private readonly string ApiKey;

        private readonly IHttpClientFactory _httpClientFactory;

        public OpenWeatherService(IOptions<OpenWeatherSettings> openWeatherSettings, IHttpClientFactory httpClientFactory)
        {
            ApiKey = openWeatherSettings.Value.ApiKey;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ResponseWeather?> GetCurrentWeatherForCity(string city)
        {
            HttpClient client = _httpClientFactory.CreateClient("weather");

            var response = await client.GetAsync($"https://api.openweathermap.org/data/2.5/weather?q={city}&units=metric&appid={ApiKey}");

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

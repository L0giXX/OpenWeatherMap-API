using OpenWeatherMap_API.Models;

namespace OpenWeatherMap_API.Services
{
    public interface IOpenWeatherService
    {
        Task<ResponseWeather?> GetCurrentWeatherForCity(string city);
    }
}

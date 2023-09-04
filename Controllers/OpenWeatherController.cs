using Microsoft.AspNetCore.Mvc;
using OpenWeatherMap_API.Services;

namespace OpenWeatherMap_API.Controllers
{
    [ApiController]

    public class OpenWeatherController : ControllerBase
    {
        private readonly IOpenWeatherService _openWeatherService;

        public OpenWeatherController(IOpenWeatherService openWeatherService)
        {
            _openWeatherService = openWeatherService;
        }


        [HttpGet("weather/{city}")]

        public async Task<IActionResult> GetWeather(string city)
        {
            var weather = await _openWeatherService.GetCurrentWeatherForCity(city);

            return weather is not null ? Ok(weather) : NotFound();
        }

    }
}

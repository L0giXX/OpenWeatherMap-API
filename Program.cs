using OpenWeatherMap_API.Models;
using OpenWeatherMap_API.Services;
namespace OpenWeatherMap_API
{
    public class Program
    {

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var CorsSettings = "_corsSettings";

            // Allow access-control-allow-origin for htmx request
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: CorsSettings, policy =>
                {
                    policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                });
            });
            builder.Services.AddMemoryCache();
            builder.Services.AddSingleton<IOpenWeatherService, OpenWeatherService>();
            builder.Services.AddTransient<CachedWeatherHandler>();
            builder.Services.AddHttpClient("weather").AddHttpMessageHandler<CachedWeatherHandler>();
            builder.Services.Configure<OpenWeatherSettings>(builder.Configuration.GetSection("OpenWeather"));

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.UseCors(CorsSettings);

            app.Run();
        }
    }
}
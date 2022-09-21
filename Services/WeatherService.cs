using Serilog;
using SerilogWebapi.Models;

namespace SerilogWebapi.Services;

public interface IWeatherService
{
    IEnumerable<WeatherForecast> GetAll();
}

public class WeatherService : IWeatherService
{
    private readonly Serilog.ILogger _log = Log.ForContext<WeatherService>();

    public IEnumerable<WeatherForecast> GetAll()
    {
        _log.Debug("WeatherService.GetAll() -> Begin");

        var summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        var forecasts = Enumerable.Range(1, 5).Select(index =>
                new WeatherForecast
                (
                    DateTime.Now.AddDays(index),
                    Random.Shared.Next(-20, 55),
                    summaries[Random.Shared.Next(summaries.Length)]
                ))
            .ToArray();

        _log.Debug("WeatherService.GetAll() -> End");

        return forecasts;
    }
}
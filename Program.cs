using JetBrains.Annotations;
using Serilog;
using SerilogWebapi.Services;

namespace SerilogWebapi;

[UsedImplicitly]
internal class Program
{
    private static int Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration)
            .Enrich.FromLogContext()
            .CreateLogger();

        Log.Logger = logger;

        try
        {
            builder.Logging.ClearProviders();
            builder.Logging.AddSerilog(logger);

            // Add services to the container.
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddSingleton<IWeatherService, WeatherService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.MapGet("/weatherforecast", (IWeatherService weatherService) =>
            {
                var log = Log.ForContext<Program>();
                log.Debug("endpoint /weatherforecast called");

                return weatherService.GetAll();
            }).WithName("GetWeatherForecast");

            app.Run();

            return 0;
        }
        catch (Exception ex)
        {
            Log.ForContext<Program>().Fatal(ex, "Startup error");
            return -1;
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
}
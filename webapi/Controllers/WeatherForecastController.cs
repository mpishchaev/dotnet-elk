using Microsoft.AspNetCore.Mvc;

namespace ElkTest.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        _logger.LogInformation("WeatherForecastController Get - this is a nice message to test the logs", DateTime.UtcNow);
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }

    [HttpGet("Error", Name = "TestError")]
    public IActionResult TestError()
    {
        try
        {
            throw new Exception("Testing exceptions - ML");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred on the action of the WeatherForecastController");
        }
        return Ok();
    }
}

using Hangfire;
using Microsoft.AspNetCore.Mvc;

namespace BackgroudJobsWithHangfire.Controllers
{
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
            //BackgroundJob.Enqueue(() => SendMessage("ayasa3d5@gmail.com"));
            Console.WriteLine(DateTime.Now);

            //BackgroundJob.Schedule(() => SendMessage("ayasa3d5@gmail.com"), TimeSpan.FromMinutes(1));

            RecurringJob.AddOrUpdate(() => SendMessage("ayasa3d5@gmail.com"), Cron.Minutely);

            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        public void SendMessage(string email)
        {
            Console.WriteLine($"Email Sent at {DateTime.Now}");
        }
    }
}

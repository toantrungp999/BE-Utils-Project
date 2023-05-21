using Utils.Application.Services.Interfaces;
using Utils.Infrastructure.HostedServices;
using Utils.Infrastructure.HostedServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace Utils.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ITestService _testService;
        private readonly QueueService<Guid> _queueService;
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(
            ITestService testService,
            QueueService<Guid> queueService,
            ILogger<WeatherForecastController> logger)
        {
            _testService = testService;
            _queueService = queueService;
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpPost("AddTestValue")]
        public async Task<IActionResult> AddTest()
        {
            var test = await _testService.AddTest();
            return Ok(test);
        }

        [HttpPost("TestBackgroundService1")]
        public async Task<IActionResult> TestBackgroundService1()
        {
            var id = Guid.NewGuid();

            var test = _queueService.GetQueue("MyService1");
            await test.QueueBackgroundWorkItemAsync(id);
      
            return Ok("Send add test in background successfully");
        }

        [HttpPost("TestBackgroundService2")]
        public async Task<IActionResult> TestBackgroundService2()
        {
            var id = Guid.NewGuid();

            var test = _queueService.GetQueue("MyService2");
            await test.QueueBackgroundWorkItemAsync(id);

            return Ok("Send add test in background successfully");
        }

        [HttpGet("GetFirstTest")]
        public IActionResult GetFirstTest()
        {
            var test = _testService.GetFirstTest();
            return Ok(test);
        }

        [HttpPut]
        public IActionResult UpdateFirstTest()
        {
            var test = _testService.UpdateFirstTest();
            return Ok(test);
        }

        
    }
}
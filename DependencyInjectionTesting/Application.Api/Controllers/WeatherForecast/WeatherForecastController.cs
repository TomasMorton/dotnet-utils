using System;
using System.Collections.Generic;
using System.Linq;
using Application.Api.SimulatedDependency;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Application.Api.Controllers.WeatherForecast
{
    [Route("[controller]")]
    public class WeatherForecastController : BaseController
    {
        private static readonly string[] Summaries =
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly IDependency _dependency;
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(IDependency dependency, ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
            _dependency = dependency;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = rng.Next(-20, 55),
                    Summary = Summaries[rng.Next(Summaries.Length)]
                })
                .ToArray();
        }
    }
}
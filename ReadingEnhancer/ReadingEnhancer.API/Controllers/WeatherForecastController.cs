using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using HttpMethod = Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http.HttpMethod;

namespace ReadingEnhancer.Controllers
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

        [HttpPost("request")]
        public async Task<IActionResult> Req(string content, string responseType, int fixation, int saccade)
        {
            var body = "";
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage
                {
                    Method = System.Net.Http.HttpMethod.Post,
                    RequestUri = new Uri("https://bionic-reading1.p.rapidapi.com/convert"),
                    Headers =
                    {
                        {"X-RapidAPI-Key", "afb7bc5b7amsh840a44eaaf446ebp1d57f1jsn36b271e187bc"},
                        {"X-RapidAPI-Host", "bionic-reading1.p.rapidapi.com"},
                    },
                    Content = new FormUrlEncodedContent(new Dictionary<string, string>
                    {
                        {
                            
                            "content", content
                        },
                        {"response_type", responseType},
                        {"request_type", "html"},
                        {"fixation", fixation.ToString()},
                        {"saccade", saccade.ToString()},
                    }),
                };
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                body = await response.Content.ReadAsStringAsync();
                Console.WriteLine(body);
            }
            catch
            {
                Console.WriteLine("Error was caught");
            }

            return Ok(body);
        }
    }
}
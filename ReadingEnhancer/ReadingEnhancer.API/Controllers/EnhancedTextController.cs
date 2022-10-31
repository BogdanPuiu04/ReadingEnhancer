using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson.IO;
using ReadingEnhancer.Application.Services;
using ReadingEnhancer.Domain.Entities;

namespace ReadingEnhancer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnhancedTextController : ControllerBase
    {
        private readonly IEnhancedTextService _enhancedService;

        public EnhancedTextController(IEnhancedTextService enhancedTextService)
        {
            _enhancedService = enhancedTextService;
        }

        [HttpPost("request")]
        public async Task<IActionResult> Req([FromBody]string content)
        {
            var body = "";
            Console.WriteLine(content);
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
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
                        {"response_type", "html"},
                        {"request_type", "html"},
                        {"fixation", "1"},
                        {"saccade", "10"},
                    }),
                };
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                body = await response.Content.ReadAsStringAsync();
            }
            catch
            {
                Console.WriteLine("Error was caught");
            }

            var res = JsonSerializer.Serialize(body);
            return Ok(res);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _enhancedService.GetAllAsync();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(EnhancedText text)
        {
            var result = await _enhancedService.AddAsync(text);
            return Ok(result);
        }

        [HttpGet("/{id}")]
        public async Task<IActionResult> Test(string id)
        {
            var test = await _enhancedService.GetAsync(id);
            return Ok(test);
        }

}
}
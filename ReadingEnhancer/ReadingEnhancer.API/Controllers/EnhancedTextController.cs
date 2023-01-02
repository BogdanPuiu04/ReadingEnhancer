using System;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReadingEnhancer.Application.Services.Interfaces;
using ReadingEnhancer.Domain.Entities;

namespace ReadingEnhancer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class EnhancedTextController : BaseController
    {
        private readonly IEnhancedTextService _enhancedService;

        public EnhancedTextController(IEnhancedTextService enhancedTextService)
        {
            _enhancedService = enhancedTextService;
        }

        [HttpPost("request")]
        public async Task<IActionResult> Req([FromBody] string content)
        {
            var res = await _enhancedService.EnhanceText(content);
            return Ok(JsonSerializer.Serialize(res.Data));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var id = GetUserBsonId();
            Console.WriteLine(id);
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

        [HttpPost("EnhanceUrl")]
        public async Task<IActionResult> EnhanceUrl([FromBody] string url)
        {
            var enhancedWebpage = await _enhancedService.EnhanceWebpage(url);
            return Ok(JsonSerializer.Serialize(enhancedWebpage.Data));
        }
    }
}
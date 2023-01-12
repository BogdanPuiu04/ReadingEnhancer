using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReadingEnhancer.Application.Models;
using ReadingEnhancer.Application.Services.Interfaces;
using ReadingEnhancer.Domain.Entities;
using JsonSerializer = System.Text.Json.JsonSerializer;

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
            var result = await _enhancedService.GetAllAsync(GetUserBsonId());
            return Ok(result.Data);
        }

        [HttpGet("GetRandomText")]
        public async Task<IActionResult> GetRandomAsync()
        {
            var result = await _enhancedService.GetRandomTextAsync();
            return Ok(result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(ReadingTextModel readingText)
        {
            var result = await _enhancedService.AddAsync(readingText);
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

        [HttpPost("ChangeText")]
        public async Task<IActionResult> ChangeText([FromBody] EnhancedText text)
        {
            var result = await _enhancedService.UpdateAsync(text.Id, text);
            return Ok(result);
        }
    }
}
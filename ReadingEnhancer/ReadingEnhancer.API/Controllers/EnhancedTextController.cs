using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReadingEnhancer.Application.Models;
using ReadingEnhancer.Application.Services.Interfaces;

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
            var result = await _enhancedService.GetAllAsync();
            return Ok(result.Data.First());
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
    }
}
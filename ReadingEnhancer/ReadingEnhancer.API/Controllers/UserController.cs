using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReadingEnhancer.Application.Models;
using ReadingEnhancer.Application.Services.Interfaces;

namespace ReadingEnhancer.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] AuthUserModel authUserModel)
        {
            var user = await _userService.Authenticate(authUserModel);
            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserModel registerUserModel)
        {
            var user = await _userService.AddAsync(registerUserModel);
            return Ok(user);
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken()
        {
            return Ok(await _userService.RefreshToken(GetUserBsonId()));
        }

        [HttpPost("SubmitResults")]
        public async Task<IActionResult> SubmitResults([FromBody] ResultsRequestModel results)
        {
            var userOk = await _userService.SubmitResult(results, GetUserBsonId());
            return Ok(userOk);
        }

        [HttpGet("GetUsersHighScore")]
        public async Task<IActionResult> GetUsersHighScores()
        {
            var response = await _userService.GetAllUsersHighScore();
            return Ok(response.Data);
        }

        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var response = await _userService.GetAllAsync(GetUserBsonId());
            return Ok(response.Data);
        }

        [HttpPost("ChangeAdminRights")]
        public async Task<IActionResult> ChangeAdmin([FromBody] string userId)
        {
            var response = await _userService.ChangeAdmin(userId, GetUserBsonId());
            return Ok(response);
        }
    }
}
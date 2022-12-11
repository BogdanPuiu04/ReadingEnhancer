using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;

namespace ReadingEnhancer.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class BaseController : ControllerBase
{
    protected string GetUserBsonId()
    {
        return HttpContext.User.FindFirst(JwtRegisteredClaimNames.Jti)?.Value;
    }
}
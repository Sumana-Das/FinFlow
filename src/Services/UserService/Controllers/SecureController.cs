using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace UserService.Controllers;

[ApiController]
[Route("api/secure")]
public class SecureController : ControllerBase
{
    [HttpGet("ping")]
    [Authorize]
    public IActionResult Ping()
    {
        var username = User.Identity?.Name;
        return Ok($"Hello {username}!, You are authenticated");
    }

    [HttpGet("admin-only")]
    [Authorize(Roles = "admin")]
    public IActionResult GetSecretDashboard()
    {
        return Ok("Welcome, Admin! 🛡️");
    }
}
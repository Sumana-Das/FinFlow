using Microsoft.AspNetCore.Mvc;
using UserService.Services;
using UserService.Models;
using Microsoft.AspNetCore.Authorization;

namespace UserService.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;

    public AuthController(IUserService userService)
    {
        _userService = userService;
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public IActionResult Register(RegisterRequest request)
    {
        var result = _userService.Register(request);
        return result ? Ok("User Registered") : Conflict("User already exists");
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public IActionResult Login(LoginRequest request)
    {
        var token = _userService.Login(request);
        return token is not null ? Ok(new { token }) : Unauthorized("Invalid credentials");
    }
}

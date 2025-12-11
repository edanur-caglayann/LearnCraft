using LearnCraftt.Application.Services.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace LearnCraftt.Api.Controller;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IAuthenticationService authService) : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto loginDto)
    {
        var result = await authService.LoginAsync(loginDto);
        return Ok(result);
    }
}

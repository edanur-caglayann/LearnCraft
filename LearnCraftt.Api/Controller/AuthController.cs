using LearnCraftt.Application.Dto.Auth;
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

    [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordRequestDto forgotPasswordRequestDto)
        {
           var result = await authService.ForgotPasswordAsync(forgotPasswordRequestDto.Email);
           return Ok(result);
        }

    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword(ResetPasswordDto resetPasswordDto)
    {
        var result = await authService.ResetPasswordAsync(resetPasswordDto);
        return Ok(result);
    }
    }


using LearnCraftt.Api.Extensions;
using LearnCraftt.Application.Dto;
using LearnCraftt.Application.Dto.Auth;
using LearnCraftt.Application.Services.Authentication;
using Microsoft.AspNetCore.Authorization;
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
 
    
    [HttpPost("send-email-verification")]
    public async Task<IActionResult> SendEmailVerification([FromBody] SendEmailVerificationRequestDto emailVerificationRequestDto)
    {
        var result = await authService.SendEmailVerificationAsync(emailVerificationRequestDto.UserId);
        return Ok(result);
    }
    
    [HttpPost("complete-email-verification")]
    public async Task<IActionResult> CompleteEmailVerification([FromBody] CompleteEmailVerificationRequestDto completeEmailVerificationRequestDto)
    {
        var result = await authService.CompleteEmailVerificationAsync(completeEmailVerificationRequestDto.Token);
        return Ok(result);
    }
    [Authorize]
    [HttpPost("change-password")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequestDto changePasswordRequestDto)
    {
        var userId = User.GetUserId();
        var result = await  authService.ChangePasswordAsync(userId, changePasswordRequestDto);
        return Ok(result);
    }

    }


using System.Security.Claims;
using LearnCraftt.Application.Dto.User;
using LearnCraftt.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LearnCraftt.Api.Controller;

[ApiController]
[Route("api/[controller]")]
public class UserController(UserService userService) : ControllerBase
{
    //[Authorize(Roles = "Admin")]
    [HttpPost("create")]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserDto createUserDto)
    { 
        var result = await userService.CreateUser(createUserDto);
        return Ok(result);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("update")]
    public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDto updateUserDto, string userId)
    {
        var result = await userService.UpdateUser(updateUserDto, userId);
        return Ok(result);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{userId}")]
    public async Task<IActionResult> DeleteUser(string userId)
    {
        var result = await userService.DeleteUser(userId);
        return Ok(result);
    }
    
    [Authorize(Roles = "Admin")]
    [HttpGet("{userId}")]
    public async Task<IActionResult> GetUserById(string userId)
    {
        var result =  await userService.GetUserById(Guid.Parse(userId));
            return Ok(result);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        var result =  await userService.GetAllUsers();
        return Ok(result);
    }

    [Authorize]
    [HttpGet("me")]
    public async Task<IActionResult> GetMe()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if(string.IsNullOrEmpty(userId)) return Unauthorized();
        
        var result = await userService.GetMyProfileAsync(Guid.Parse(userId));
        if(!result.Success) return BadRequest(result.Message);
        return Ok(result.Data);
    }
}

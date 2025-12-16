using System.Security.Claims;
using LearnCraftt.Application.Dto.Content;
using LearnCraftt.Application.Services.Contents;
using Microsoft.AspNetCore.Mvc;

namespace LearnCraftt.Api.Controller;

[ApiController]
[Route("api/[controller]")]
public class ContentController(IContentService contentService) : ControllerBase
{
    [HttpPost("create")]
    public async Task<ActionResult> CreateContent([FromBody] CreateContentRequestDto createContentDto)
    {
        // JWT token'dan bu istegi yapan kullanicinin ID'sini al
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var result = await contentService.CreateAsync(createContentDto, userId);
        return Ok(result);
    }
    
    [HttpPut("update")]
    public async Task<IActionResult> UpdateContent([FromBody] UpdateContentRequestDto updateContentDto)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var result = await contentService.UpdateAsync(updateContentDto, userId);
        return Ok(result);
    }

    [HttpDelete("{contentId}")]
    public async Task<IActionResult> DeleteContent(Guid contentId)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var result = await contentService.DeleteAsync(contentId, userId);
        return Ok(result);

    }

    [HttpGet("{contentId}")]
    public async Task<IActionResult> GetContentById(Guid contentId)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var result = await contentService.GetContentById(contentId, userId);
        return Ok(result);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllContents()
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var result = await contentService.GetAllContents(userId);
        return Ok(result);
    }
}
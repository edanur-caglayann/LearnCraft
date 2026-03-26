using LearnCraftt.Application.Dto.Content;
using LearnCraftt.Application.Services.Analysis;
using Microsoft.AspNetCore.Mvc;

namespace LearnCraftt.Api.Controller;

[ApiController]
[Route("api/[controller]")]
public class AIController(IContentAnalysisService contentAnalysisService) : ControllerBase
{
    [HttpPost("analyze")]
    public async Task<IActionResult> GenerateAnalyze([FromBody] AnalyzeContentRequestDto analyzeContentDto)
    {
        var result = await contentAnalysisService.GenerateAnalyzeAsync(analyzeContentDto.Text);
        return Ok(result);
    }
    
}
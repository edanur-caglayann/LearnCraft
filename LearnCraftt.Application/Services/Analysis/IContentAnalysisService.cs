using LearnCraftt.Application.Dto.Content;

namespace LearnCraftt.Application.Services.Analysis;

public interface IContentAnalysisService
{
    Task<AIResponseDto> GenerateAnalyzeAsync(string text);
}
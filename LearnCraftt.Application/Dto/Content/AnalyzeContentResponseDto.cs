namespace LearnCraftt.Application.Dto.Content;

public class AnalyzeContentResponseDto
{
    public Guid ContentId { get; set; }
    public string Summary { get; set; } = null!;
    public List <string> Tags { get; set; } = null!;
    public DateTime AnalyzedAt { get; set; }
}
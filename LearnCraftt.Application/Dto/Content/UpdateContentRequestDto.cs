using System.Text.Json;
using LearnCraftt.Domain.Enums;

namespace LearnCraftt.Application.Dto.Content;

public class UpdateContentRequestDto
{
    public string Title { get; set; }  = null!;
    public string Text { get; set; }  = null!;
    public string Category { get; set; }
    public JsonDocument? Format { get; set; }
    public ContentFormat ContentType { get; set; }
    public long Size { get; set; }
}
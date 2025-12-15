using System.Net.Mime;
using System.Text.Json;
using LearnCraftt.Domain.Enums;

namespace LearnCraftt.Application.Dto.Content;

public class CreateContentRequestDto
{

    public string Title { get; set; }  = null!;
    public string Text { get; set; }  = null!;
    public required string Category { get; set; }
    public JsonDocument? Format { get; set; }
    public required ContentFormat ContentType { get; set; }
    public long Size { get; set; }
}
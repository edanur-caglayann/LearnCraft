using System.Text.Json;
using LearnCraftt.Domain.Models;

namespace LearnCraftt.Domain.Entities;

public class UploadedContent : BaseEntity
{
    public int UserId{ get; set; }
    public required string Category { get; set; }
    public JsonDocument Format { get; set; }
    public required string ContentType { get; set; }
    public long Size { get; set; }
    
}
using System.Text.Json;
using LearnCraftt.Domain.Models;

namespace LearnCraftt.Domain.Entities;

public class AIFormat : BaseEntity
{
    public string Title { get; set; }
    public JsonDocument Format { get; set; }
    public string Category { get; set; }
    public string Description { get; set; }
}
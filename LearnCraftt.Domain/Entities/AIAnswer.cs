using LearnCraftt.Domain.Models;

namespace LearnCraftt.Domain.Entities;

public class AIAnswer : BaseEntity
{
    public int AIFormatId { get; set; }
    public int AnswerId { get; set; }
    public bool IsCorrect { get; set; }
}

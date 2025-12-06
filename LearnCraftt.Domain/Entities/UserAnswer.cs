using LearnCraftt.Domain.Models;

namespace LearnCraftt.Domain.Entities;

public class UserAnswer : BaseEntity
{
    public int UserId { get; set; }
    public int AiQuestionId { get; set; }
    public int AiAnswerId { get; set; }
    
}
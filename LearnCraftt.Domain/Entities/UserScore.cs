using LearnCraftt.Domain.Models;

namespace LearnCraftt.Domain.Entities;

public class UserScore : BaseEntity
{
    public int UserId { get; set; }
    public int AiFormatId { get; set; }
    public required string Category { get; set; }
    public int Score { get; set; }
    public int TotalSolvedQuestions { get; set; }
    public int TotalExam { get; set; }
}
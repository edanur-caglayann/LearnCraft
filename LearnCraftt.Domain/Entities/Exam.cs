using LearnCraftt.Domain.Models;

namespace LearnCraftt.Domain.Entities;

public class Exam : BaseEntity
{
    public int UserId { get; set; }
    public int UploadedContentId { get; set; }
    public int TotalQuestions { get; set; }
    public int TotalScore { get; set; }
    public int NumberOfCorrect { get; set; }
    public int NumberOfIncorrect { get; set; }
    public int NumberOfBlank { get; set; }
    public string State { get; set; }
    
}

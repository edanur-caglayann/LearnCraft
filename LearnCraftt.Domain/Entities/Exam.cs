using LearnCraftt.Domain.Enums;
using LearnCraftt.Domain.Models;

namespace LearnCraftt.Domain.Entities;

public class Exam : BaseEntity
{
    public int UserId { get; set; }
    public int UploadedContentId { get; set; }
    public int TotalQuestions { get; set; } = 0;
    public int TotalScore { get; set; } = 0;
    public int NumberOfCorrect { get; set; } = 0;
    public int NumberOfIncorrect { get; set; } = 0;
    public int NumberOfBlank { get; set; } = 0;
    public ExamStatus State { get; set; } = ExamStatus.Active;

}

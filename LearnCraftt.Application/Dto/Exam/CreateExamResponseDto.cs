using LearnCraftt.Domain.Enums;

namespace LearnCraftt.Application.Dto.Exam;

public class CreateExamResponseDto
{
    public int ExamId { get; set; }
    public int UploadedContentId { get; set; }

    public ExamStatus State { get; set; }

    public int TotalQuestions { get; set; }
    public int TotalScore { get; set; }

    public DateTime CreatedAt { get; set; }
}
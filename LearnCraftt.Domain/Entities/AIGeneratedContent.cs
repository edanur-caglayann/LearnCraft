using LearnCraftt.Domain.Models;

namespace LearnCraftt.Domain.Entities;


public class AIGeneratedContent : BaseEntity
{
    public int UserId { get; set; }
    public int ExamId { get; set; }
    public int QuestionId { get; set; }
    public int UploadedContentId { get; set; }
    public int AIFormatId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    
}
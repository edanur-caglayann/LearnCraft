using LearnCraftt.Domain.Models;

namespace LearnCraftt.Domain.Entities;

public class AIQuestion : BaseEntity
{
    public int UploadedContentId { get; set; }
    public int ExamId { get; set; }
    public int AIFormatId { get; set; }
    
}
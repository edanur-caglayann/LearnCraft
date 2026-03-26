using System.Net.Mime;
using System.Text.Json;
using LearnCraftt.Domain.Enums;
using LearnCraftt.Domain.Models;
   
namespace LearnCraftt.Domain.Entities;
   
public class Content : BaseEntity
{
    public Guid UserId{ get; set; }
    public string Title { get; set; }  = null!;
    public string Text { get; set; }  = null!;
    public string? Category { get; set; }
    public JsonDocument? Format { get; set; }
    public int ContentType { get; set; }
    public long Size { get; set; }
    
    
    public bool IsAnalyzed { get; set; } 
    public DateTime? AnalyzedAt { get; set; }
    public string? Summary { get; set; } // icerigin kisa ozeti(ai uretecek)
    public List <string> Tags { get; set; } // anahtar kelimler / etiketler

   
    public User User { get; set; } = null!;
   
}
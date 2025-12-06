namespace LearnCraftt.Domain.Models;

public class BaseEntity
{
    public Guid Id {get; set;}
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedDate {get; set;} = DateTime.UtcNow;
 
}
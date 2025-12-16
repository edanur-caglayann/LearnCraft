using LearnCraftt.Domain.Models;

namespace LearnCraftt.Domain.Entities;

public class EmailConfirmationToken : BaseEntity
{
    public required Guid UserId { get; set; }
    public string TokenHash { get; set; } = null!;
    public DateTime ExpireAt { get; set; }
    public bool IsUsed { get; set; }
}
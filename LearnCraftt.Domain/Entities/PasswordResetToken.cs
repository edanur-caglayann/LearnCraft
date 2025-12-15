using LearnCraftt.Domain.Models;

namespace LearnCraftt.Domain.Entities;

// sifre sifirlama icin gecici ve kalici token'lari yonetmek icin
public class PasswordResetToken : BaseEntity
{
    public Guid UserId { get; set; }
    public required string TokenHash { get; set; }
    public DateTime ExpireAt { get; set; }
    public bool IsUsed { get; set; }
}
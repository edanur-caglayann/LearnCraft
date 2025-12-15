using LearnCraftt.Domain.Enums;
using LearnCraftt.Domain.Models;

namespace LearnCraftt.Domain.Entities;

public class User : BaseEntity
{
    public required string UserName { get; set; }
    public required string UserSurname { get; set; }
    public required string Email { get; set; }
    public required string PasswordHash { get; set; }
    public bool IsEmailConfirmed { get; set; }
    public string? ProfileImage { get; set; } = null!;
    
    //DB’de her user’ın bir rolü olacak. Yeni oluşturulan kullanıcı default User olur.
    public UserRole Role { get; set; } = UserRole.User;
    
}
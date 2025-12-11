namespace LearnCraftt.Application.Dto.User;

public class UpdateUserDto
{
    public required string UserName { get; set; }
    public required string UserSurname { get; set; }
    public required string Email { get; set; }
    public required string CurrentPassword { get; set; } // sifre doogrulama
    public string? NewPassword { get; set; } // degistirilecek sifre
    public string? ProfileImage { get; set; } = null!;
}
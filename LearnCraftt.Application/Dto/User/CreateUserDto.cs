namespace LearnCraftt.Application.Dto.User;

public class CreateUserDto
{
    public required string UserName { get; set; }
    public required string UserSurname { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public string? ProfileImage { get; set; }
}
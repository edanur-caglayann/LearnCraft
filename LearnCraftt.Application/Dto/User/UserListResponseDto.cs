namespace LearnCraftt.Application.Dto.User;

public class UserListResponseDto
{
    public Guid Id { get; set; }
    public required string Email { get; set; }
    public required string UserName { get; set; }
    public required string UserSurname { get; set; }
    public string? ProfileImage { get; set; }
}
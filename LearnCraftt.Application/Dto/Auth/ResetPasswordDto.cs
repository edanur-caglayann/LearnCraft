namespace LearnCraftt.Application.Dto.Auth;

public class ResetPasswordDto
{ 
    public required string Token { get; set; }
    public required string NewPassword { get; set; }
}
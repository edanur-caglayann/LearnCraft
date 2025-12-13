namespace LearnCraftt.Application.Services.Authentication;

// kullanici login olunca dondurulecek olan veriler

public class TokenResponse
{
    public required string Token { get; set; }
    public DateTime EndDate { get; set; }

}
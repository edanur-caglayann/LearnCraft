using LearnCraftt.Domain.Entities;

namespace LearnCraftt.Application.Services.Authentication;

public interface ITokenService
{
    TokenResponse GenerateToken(User user);
}
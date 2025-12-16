using LearnCraftt.Domain.Entities;

namespace LearnCraftt.Application.Repositories.PasswordResetTokenRepositories;

public interface IPasswordResetTokenReadRepository : IReadRepository<PasswordResetToken>
{
    Task<PasswordResetToken?> GetValidTokenAsync(string tokenHash);
}
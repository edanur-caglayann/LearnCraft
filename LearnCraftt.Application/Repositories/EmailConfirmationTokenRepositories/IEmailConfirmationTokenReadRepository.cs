using LearnCraftt.Domain.Entities;

namespace LearnCraftt.Application.Repositories.EmailConfirmationTokenRepositories;

public interface IEmailConfirmationTokenReadRepository : IReadRepository<EmailConfirmationToken>
{
    // mail dogrulama linkinden gelen token'in gecerli olup olmadigini tek noktadan kontrol eder
    Task<EmailConfirmationToken?> GetValidTokenAsync(string tokenHash);

}
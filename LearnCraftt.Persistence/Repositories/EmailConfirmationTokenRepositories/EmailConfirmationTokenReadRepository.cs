using LearnCraftt.Application.Repositories.EmailConfirmationTokenRepositories;
using LearnCraftt.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LearnCraftt.Persistence.Repositories.EmailConfirmationTokenRepositories;

public class EmailConfirmationTokenReadRepository : ReadRepository<EmailConfirmationToken>, IEmailConfirmationTokenReadRepository
{
    public EmailConfirmationTokenReadRepository(LearnCrafttDbContext context) : base(context)
    {
    }

    public async Task<EmailConfirmationToken?> GetValidTokenAsync(string tokenHash)
    {
        // token db'de var mi, daha once kullanilmis mi, suresi dolmus mu
        return await Table.FirstOrDefaultAsync(x =>
            x.TokenHash == tokenHash &&
            !x.IsUsed &&
            x.ExpireAt > DateTime.UtcNow);
    }
}
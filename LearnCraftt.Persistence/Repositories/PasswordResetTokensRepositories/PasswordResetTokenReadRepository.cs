using System.Linq.Expressions;
using LearnCraftt.Application.Repositories.PasswordResetTokenRepositories;
using LearnCraftt.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LearnCraftt.Persistence.Repositories.PasswordResetTokensRepositories;

public class PasswordResetTokenReadRepository : ReadRepository<PasswordResetToken>, IPasswordResetTokenReadRepository
{
    public PasswordResetTokenReadRepository(LearnCrafttDbContext context) : base(context)
    {
    }
    //db'de reset token arar bulursa doner bulamazsa null doner
    public async Task<PasswordResetToken?> GetValidTokenAsync(string tokenHash)
    {
        return await Table.FirstOrDefaultAsync(x =>
            x.TokenHash == tokenHash && // Linkten gelen token ile DB’deki hash eşleşiyor mu
            !x.IsUsed && // token daha once kullanilmis mi
            x.ExpireAt > DateTime.UtcNow); // token sureci dolmus mu
    }
}
using LearnCraftt.Application.Repositories.PasswordResetTokenRepositories;
using LearnCraftt.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LearnCraftt.Persistence.Repositories.PasswordResetTokensRepositories;

public class PasswordResetTokenWriteRepository : WriteRepository<PasswordResetToken>, IPasswordResetTokenWriteRepository
{
    public PasswordResetTokenWriteRepository(LearnCrafttDbContext context) : base(context)
    {
    }
    
}
using LearnCraftt.Application.Repositories.EmailConfirmationTokenRepositories;
using LearnCraftt.Domain.Entities;

namespace LearnCraftt.Persistence.Repositories.EmailConfirmationTokenRepositories;

public class EmailConfirmationTokenWriteRepository : WriteRepository<EmailConfirmationToken>, IEmailConfirmationTokenWriteRepository
{
    public EmailConfirmationTokenWriteRepository(LearnCrafttDbContext context) : base(context)
    {
    }
}
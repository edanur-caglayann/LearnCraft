using LearnCraftt.Application.Repositories.AIFormatRepositories;
using LearnCraftt.Domain.Entities;

namespace LearnCraftt.Persistence.Repositories.AIFormatRepositories;

public class AIFormatReadRepository : ReadRepository<AIFormat>, IAIFormatReadRepository
{
    public AIFormatReadRepository(LearnCrafttDbContext context) : base(context)
    {
    }
}
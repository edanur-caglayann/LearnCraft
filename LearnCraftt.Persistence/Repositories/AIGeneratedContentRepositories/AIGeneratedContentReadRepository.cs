using LearnCraftt.Application.Repositories.AIGeneratedContentRepositories;
using LearnCraftt.Domain.Entities;

namespace LearnCraftt.Persistence.Repositories.AIGeneratedContentRepositories;

public class AIGeneratedContentReadRepository: ReadRepository<AIGeneratedContent>, IAIGeneratedContentReadRepository
{
    public AIGeneratedContentReadRepository(LearnCrafttDbContext context) : base(context)
    {
    }
}
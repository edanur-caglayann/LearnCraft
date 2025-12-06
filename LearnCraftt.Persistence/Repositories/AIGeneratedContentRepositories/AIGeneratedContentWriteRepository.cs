using LearnCraftt.Application.Repositories.AIGeneratedContentRepositories;
using LearnCraftt.Domain.Entities;

namespace LearnCraftt.Persistence.Repositories.AIGeneratedContentRepositories;

public class AIGeneratedContentWriteRepository: WriteRepository<AIGeneratedContent>, IAIGeneratedContentWriteRepository
{
    public AIGeneratedContentWriteRepository(LearnCrafttDbContext context) : base(context)
    {
    }
}
using LearnCraftt.Application.Repositories.AIAnswerRepositories;
using LearnCraftt.Domain.Entities;

namespace LearnCraftt.Persistence.Repositories.AIAnswerRepositories;

public class AIAnswerReadRepository : ReadRepository<AIAnswer>, IAIAnswerReadRepository
{
    public AIAnswerReadRepository(LearnCrafttDbContext context) : base(context)
    {
    }
}
using LearnCraftt.Application.Repositories.AIAnswerRepositories;
using LearnCraftt.Domain.Entities;

namespace LearnCraftt.Persistence.Repositories.AIAnswerRepositories;

public class AIAnswerWriteRepository: WriteRepository<AIAnswer>, IAIAnswerWriteRepository
{
    public AIAnswerWriteRepository(LearnCrafttDbContext context) : base(context)
    {
    }
}
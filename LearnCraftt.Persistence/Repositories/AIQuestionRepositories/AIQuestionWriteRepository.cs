using LearnCraftt.Application.Repositories.AIQuestionRepositories;
using LearnCraftt.Domain.Entities;

namespace LearnCraftt.Persistence.Repositories.AIQuestionRepositories;

public class AIQuestionWriteRepository: WriteRepository<AIQuestion>, IAIQuestionWriteRepository
{
    public AIQuestionWriteRepository(LearnCrafttDbContext context) : base(context)
    {
    }
}
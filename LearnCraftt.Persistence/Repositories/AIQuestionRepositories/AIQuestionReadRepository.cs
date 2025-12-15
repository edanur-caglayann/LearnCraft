using LearnCraftt.Application.Repositories.AIQuestionRepositories;
using LearnCraftt.Domain.Entities;

namespace LearnCraftt.Persistence.Repositories.AIQuestionRepositories;

public class AIQuestionReadRepository: ReadRepository<AIQuestion>, IAIQuestionReadRepository
{
    public AIQuestionReadRepository(LearnCrafttDbContext context) : base(context)
    {
    }
    
}
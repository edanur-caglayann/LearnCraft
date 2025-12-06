using LearnCraftt.Application.Repositories.AIFormatRepositories;
using LearnCraftt.Domain.Entities;

namespace LearnCraftt.Persistence.Repositories.AIFormatRepositories;

public class AIFormatWriteRepository: WriteRepository<AIFormat>, IAIFormatWriteRepository
{
    public AIFormatWriteRepository(LearnCrafttDbContext context) : base(context)
    {
    }
}
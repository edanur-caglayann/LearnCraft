using LearnCraftt.Application.Repositories.ExamRepositories;
using LearnCraftt.Domain.Entities;

namespace LearnCraftt.Persistence.Repositories.ExamRepositories;

public class ExamReadRepository: ReadRepository<Exam>, IExamReadRepository
{
    public ExamReadRepository(LearnCrafttDbContext context) : base(context)
    {
    }
    
}
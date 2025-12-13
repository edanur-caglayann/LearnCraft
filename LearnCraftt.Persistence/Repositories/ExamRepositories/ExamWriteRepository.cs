using LearnCraftt.Application.Repositories.ExamRepositories;
using LearnCraftt.Domain.Entities;

namespace LearnCraftt.Persistence.Repositories.ExamRepositories;

public class ExamWriteRepository: WriteRepository<Exam>, IExamWriteRepository
{
    public ExamWriteRepository(LearnCrafttDbContext context) : base(context)
    {
    }
}
using LearnCraftt.Application.Repositories.UserAnswerRepositories;
using LearnCraftt.Domain.Entities;

namespace LearnCraftt.Persistence.Repositories.UserAnswerRepositories;

public class UserAnswerWriteRepository: WriteRepository<UserAnswer>, IUserAnswerWriteRepository
{
    public UserAnswerWriteRepository(LearnCrafttDbContext context) : base(context)
    {
    }
}
using LearnCraftt.Application.Repositories.UserAnswerRepositories;
using LearnCraftt.Domain.Entities;

namespace LearnCraftt.Persistence.Repositories.UserAnswerRepositories;

public class UserAnswerReadRepository: ReadRepository<UserAnswer>, IUserAnswerReadRepository
{
    public UserAnswerReadRepository(LearnCrafttDbContext context) : base(context)
    {
    }
    
}
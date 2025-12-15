using LearnCraftt.Application.Repositories.UserScoreRepositories;
using LearnCraftt.Domain.Entities;

namespace LearnCraftt.Persistence.Repositories.UserScoreRepositories;

public class UserScoreReadRepository : ReadRepository<UserScore>, IUserScoreReadRepository
{
    public UserScoreReadRepository(LearnCrafttDbContext context) : base(context)
    {
    }
}
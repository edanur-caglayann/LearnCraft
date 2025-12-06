using LearnCraftt.Application.Repositories.UserScoreRepositories;
using LearnCraftt.Domain.Entities;

namespace LearnCraftt.Persistence.Repositories.UserScoreRepositories;

public class UserScoreWriteRepository : WriteRepository<UserScore>, IUserScoreWriteRepository
{
    public UserScoreWriteRepository(LearnCrafttDbContext context) : base(context)
    {
    }
}
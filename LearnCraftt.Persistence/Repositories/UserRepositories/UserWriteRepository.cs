using LearnCraftt.Application.Repositories.UserRepositories;
using LearnCraftt.Domain.Entities;

namespace LearnCraftt.Persistence.Repositories.UserRepositories;

public class UserWriteRepository : WriteRepository<User>, IUserWriteRepository
{
    public UserWriteRepository(LearnCrafttDbContext context) : base(context)
    {
    }
}
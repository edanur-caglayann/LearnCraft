using LearnCraftt.Application.Repositories.UserRepositories;
using LearnCraftt.Domain.Entities;

namespace LearnCraftt.Persistence.Repositories.UserRepositories;

public class UserReadRepository : ReadRepository<User>, IUserReadRepository
{
    public UserReadRepository(LearnCrafttDbContext context) : base(context)
    {
    }
}
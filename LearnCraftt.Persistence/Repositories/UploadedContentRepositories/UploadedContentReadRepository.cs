using LearnCraftt.Application.Repositories.UploadedContentRepositories;
using LearnCraftt.Domain.Entities;

namespace LearnCraftt.Persistence.Repositories.UploadedContentRepositories;

public class UploadedContentReadRepository: ReadRepository<UploadedContent>, IUploadedContentReadRepository
{
    public UploadedContentReadRepository(LearnCrafttDbContext context) : base(context)
    {
    }
    
}
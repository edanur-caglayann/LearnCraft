using LearnCraftt.Application.Repositories.UploadedContentRepositories;
using LearnCraftt.Domain.Entities;

namespace LearnCraftt.Persistence.Repositories.UploadedContentRepositories;

public class ContentReadRepository: ReadRepository<Content>, IContentReadRepository
{
    public ContentReadRepository(LearnCrafttDbContext context) : base(context)
    {
    }
    
}
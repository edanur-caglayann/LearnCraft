using LearnCraftt.Application.Repositories.UploadedContentRepositories;
using LearnCraftt.Domain.Entities;

namespace LearnCraftt.Persistence.Repositories.UploadedContentRepositories;

public class ContentWriteRepository: WriteRepository<Content>, IContentWriteRepository
{
    public ContentWriteRepository(LearnCrafttDbContext context) : base(context)
    {
    }
}
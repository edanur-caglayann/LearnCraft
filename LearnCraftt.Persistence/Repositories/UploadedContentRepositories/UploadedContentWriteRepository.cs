using LearnCraftt.Application.Repositories.UploadedContentRepositories;
using LearnCraftt.Domain.Entities;

namespace LearnCraftt.Persistence.Repositories.UploadedContentRepositories;

public class UploadedContentWriteRepository: WriteRepository<UploadedContent>, IUploadedContentWriteRepository
{
    public UploadedContentWriteRepository(LearnCrafttDbContext context) : base(context)
    {
    }
}
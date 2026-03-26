using LearnCraftt.Application.Common;
using LearnCraftt.Application.Dto.Content;
using LearnCraftt.Domain.Entities;

namespace LearnCraftt.Application.Services.Contents;

public interface IContentService
{
    Task<ServiceResult<Guid>> CreateAsync(CreateContentRequestDto createContent, Guid userId);

    Task<ServiceResult<Guid>> UpdateAsync(UpdateContentRequestDto updateContent, Guid userId);
    
    Task<ServiceResult<Guid>> DeleteAsync(Guid contentId, Guid userId);
    
    Task<ServiceResult<ContentResponseDto>> GetContentById(Guid contentId, Guid userId);
    Task<ServiceResult<List<ContentResponseDto>>>GetAllContents(Guid userId);
    

}
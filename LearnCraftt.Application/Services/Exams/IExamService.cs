using LearnCraftt.Application.Common;
using LearnCraftt.Application.Dto.Exam;

namespace LearnCraftt.Application.Services.Exams;

public interface IExamService
{
    Task<ServiceResult<CreateExamResponseDto>> CreateExamAsync(int uploadedContentId, int userId);
}
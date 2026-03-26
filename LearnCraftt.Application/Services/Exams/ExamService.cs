/*using LearnCraftt.Application.Common;
using LearnCraftt.Application.Dto.Exam;
using LearnCraftt.Application.Repositories.ExamRepositories;
using LearnCraftt.Application.Repositories.UploadedContentRepositories;

namespace LearnCraftt.Application.Services.Exams;

public class ExamService(
    IContentReadRepository contentRead,
    IExamReadRepository examRead,
    IExamWriteRepository examWrite): IExamService
{
    public Task<ServiceResult<CreateExamResponseDto>> CreateExamAsync(int uploadedContentId, int userId)
    {
        // content var mi?
        var content = contentRead.GetByIdAsync(uploadedContentId);
        if (content == null)
            return ServiceResult<CreateExamResponseDto>.FailResult("Content not found");
        
        
         } 
}  
/*
 * createexam
 * 
 * Content var mı?
   Content user’a mı ait?
   Aynı content için aktif exam var mı?
   Yeni Exam oluşturur
   
   GetUserExamsAsync
   GetExamDetailAsync
   Exam user’a mı ait?
   Exam bilgileri
   Content bilgisi
   Toplam soru sayısı
   
   GetExamForSolveAsync
   
   MarkExamAsCompletedAsync
   
   CheckExamOwnershipAsync
   CheckExamIsActiveAsync
   
   GetExamContentIdAsync
 * 
*/
using System.Net.Mime;
using System.Security.Cryptography;
using LearnCraftt.Application.Common;
using LearnCraftt.Application.Dto.Content;
using LearnCraftt.Application.Repositories.UploadedContentRepositories;
using LearnCraftt.Application.Repositories.UserRepositories;
using LearnCraftt.Application.Services.Analysis;
using LearnCraftt.Domain.Entities;
using LearnCraftt.Domain.Enums;
using Microsoft.EntityFrameworkCore.ChangeTracking;


namespace LearnCraftt.Application.Services.Contents;

public class ContentService(
    IContentReadRepository contentRead,
    IContentWriteRepository contentWrite,
    IUserReadRepository userRead,
    IContentAnalysisService contentAnalysisService) : IContentService
{
    //Kullanıcının gönderdiği içeriği doğrula → kaydet → AI ile analiz et → sonucu DB’ye yaz
    public async Task<ServiceResult<Guid>> CreateAsync(CreateContentRequestDto createContent, Guid userId)
    {
        // icerik olusturan kullanici db' de var mi (token'da gelen ile karislasitir)
        var existingUser = await userRead.GetSingleAsync(x => x.Id == userId);
        if (existingUser == null)
            return ServiceResult<Guid>.FailResult("User could not be found.");

        // bos/anlamsiz icerik kontorlu. kullanici bosluk yazip yollayamasin
        if (string.IsNullOrWhiteSpace(createContent.Text))
            return ServiceResult<Guid>.FailResult("Content text cannot be empty.");
        if (string.IsNullOrWhiteSpace(createContent.Title))
            return ServiceResult<Guid>.FailResult("Title cannot be empty.");

        // title zorunlu ve normalize edilmis olmali "abc" ile " abc " ayni olmali
        var title = createContent.Title
            ?.Trim(); // Trim -> veri normalizasyonu. string basindaki ve sonundaki bosluklari siler
        if (string.IsNullOrWhiteSpace(title)) // trim sonrasi tekrar "" bos kalmis olabilri diye kontrol
            return ServiceResult<Guid>.FailResult("Title is required..");

        // text xorunlu ve anlamli olmali 
        var text = createContent.Text?.Trim();
        if (string.IsNullOrWhiteSpace(text))
            return ServiceResult<Guid>.FailResult("Content text is required.");

        // kategori zorunlu ve normalize olmali
        var category = createContent.Category?.Trim();
        if (string.IsNullOrWhiteSpace(category))
            return ServiceResult<Guid>.FailResult("Category is required.");

        // farkli icerik turelerine gore farkli metadatalar yolla
        switch (createContent.ContentType)
        {
            case ContentFormat.Text:
                if (createContent.Format != null)
                    return ServiceResult<Guid>.FailResult("Format must be empty for text content.");
                break;
            //sayfa sayisi, boyut, counurluk giib metadata ister
            case ContentFormat.Pdf:
            case ContentFormat.Word:
            case ContentFormat.Image:
                if (createContent.Format == null)
                    return ServiceResult<Guid>.FailResult("Format metadata is required for this content type.");
                break;
        }

        // boyut/kapasite kontorlu
        const int MaxLength = 100_000;
        if (text.Length > MaxLength)
            return ServiceResult<Guid>.FailResult("Text is too long.");
        if (createContent.Size < 0)
            return ServiceResult<Guid>.FailResult("Invalid content size.");

        var content = new Content
        {
            UserId = userId,
            Title = title,
            Text = text,
            Category = category,
            Format = createContent.Format,
            ContentType = (int)createContent.ContentType,
            Size = createContent.Size,
            IsAnalyzed = false,
        };
        
        await contentWrite.AddAsync(content);
        await contentWrite.SaveAsync();

        try
        {
            var aiResult =
                await contentAnalysisService.GenerateAnalyzeAsync(content.Text);

            content.Summary = aiResult.Summary;

            content.Tags = aiResult.Tags != null && aiResult.Tags.Any()
                ? aiResult.Tags
                : new List<string> { "General" };

            content.IsAnalyzed = true;

            await contentWrite.UpdateAsync(content);
            await contentWrite.SaveAsync();

        }
        catch (Exception)
        {
            content.IsAnalyzed = false;
            content.Summary = null;
            content.Tags = null;

            await contentWrite.UpdateAsync(content);
            await contentWrite.SaveAsync();
        }
        

        return ServiceResult<Guid>.SuccessResult(content.Id);
    }

    public async Task<ServiceResult<Guid>> UpdateAsync(UpdateContentRequestDto updateContent, Guid userId)
    {
        var existingUser = await userRead.GetSingleAsync(x => x.Id == userId);
        if (existingUser == null)
            return ServiceResult<Guid>.FailResult("User could not be found.");

        var existingContent = await contentRead.GetSingleAsync(x => x.Title == updateContent.Title);
        if (existingContent == null)
            return ServiceResult<Guid>.FailResult("Content could not be found.");

        existingContent.Title = updateContent.Title;
        existingContent.Text = updateContent.Text;
        existingContent.Category = updateContent.Category;
        existingContent.Format = updateContent.Format;
        existingContent.Size = updateContent.Size;

        await contentWrite.UpdateAsync(existingContent);
        await contentWrite.SaveAsync();
        return ServiceResult<Guid>.SuccessResult(existingContent.Id);
    }

    public async Task<ServiceResult<Guid>> DeleteAsync(Guid contentId, Guid userId)
    {
        var existingContent = await contentRead.GetSingleAsync(x => x.Id == contentId);
        if (existingContent == null)
            return ServiceResult<Guid>.FailResult("Content could not be found.");

        if (existingContent.UserId != userId)
            return ServiceResult<Guid>.FailResult("You are not allowed to delete this content.");
        
        contentWrite.Delete(existingContent);
        await contentWrite.SaveAsync();

        return ServiceResult<Guid>.SuccessResult(existingContent.Id);
    }

    public async Task<ServiceResult<ContentResponseDto>> GetContentById(Guid contentId, Guid userId)
    {
        var existingContent = await contentRead.GetSingleAsync(x => x.Id == contentId);
        if(existingContent == null)
            return ServiceResult<ContentResponseDto>.FailResult("Content could not be found.");

        if(existingContent.UserId != userId)
            return ServiceResult<ContentResponseDto>.FailResult("You are not allowed to view this content.");
 
        var response = new ContentResponseDto
        {
            Title = existingContent.Title,
            Text = existingContent.Text,
            Format = existingContent.Format,
            Size = existingContent.Size,
            Category = existingContent.Category,
            ContentType = (ContentFormat) existingContent.ContentType,
        };
        return ServiceResult<ContentResponseDto>.SuccessResult(response);
    }

    public async Task<ServiceResult<List<ContentResponseDto>>> GetAllContents(Guid userId)
    {
        var contents = contentRead.GetAll().Where(x=> x.UserId == userId);
        var contentList = contents.Select(x => new ContentResponseDto
        {
            
            Title = x.Title,
            Text = x.Text,
            Format = x.Format,
            Size = x.Size,
            Category = x.Category,
            ContentType = (ContentFormat)x.ContentType
        }).ToList();
        
        return ServiceResult<List<ContentResponseDto>>.SuccessResult(contentList);
    }

 
    /*
    public async Task<ServiceResult<AnalyzeContentResponseDto>> AnalyzeContent(Guid contentId, Guid userId)
    {
        var existingContent = await contentRead.GetSingleAsync(x => x.Id == contentId);
        if(existingContent == null)
            return ServiceResult<AnalyzeContentResponseDto>.FailResult("Content could not be found.");
        
        if(existingContent.UserId != userId)
            return ServiceResult<AnalyzeContentResponseDto>.FailResult("You are not allowed to view this content.");
        
        //daha once analiz edilmis mi
        if(existingContent.IsAnalyzed)
            return ServiceResult<AnalyzeContentResponseDto>.FailResult("Content is already analyzed.");
        
        var summary = await contentAnalysisService.GenerateAnalyze(existingContent.Text);
        
        // ai'dan donen sonucu yazar
        existingContent.Summary = summary.Summary;
        existingContent.Tags = summary.Tags;

        existingContent.IsAnalyzed = true;
        existingContent.AnalyzedAt = DateTime.UtcNow;
        
        await contentWrite.UpdateAsync(existingContent);
        await contentWrite.SaveAsync();
        var response = new AnalyzeContentResponseDto
        {
            ContentId = existingContent.Id,
            Summary = existingContent.Summary,
            Tags = existingContent.Tags,
            AnalyzedAt = existingContent.AnalyzedAt!.Value
        };
        
        return ServiceResult<AnalyzeContentResponseDto>.SuccessResult(response);
    }
    */
     }
    
    /*
     * 1️⃣ Gerçek OpenAI API çağrısı
       
       prompt yazımı
       
       HttpClient
       
       token handling
       
       2️⃣ Analyze sonrası otomatik soru üretimi
       
       GenerateQuestions()
       
       Exam entity
       
       3️⃣ Analyze işlemini background job yapma
       
       “Analyze queued” yaklaşımı
     */

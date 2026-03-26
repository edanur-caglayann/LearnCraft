using System.Net.Http.Json;
using System.Text.Json;
using LearnCraftt.Application.Common.Options;
using LearnCraftt.Application.Dto.Content;

namespace LearnCraftt.Application.Services.Analysis;

public class ContentAnalysisService(
    HttpClient httpClient,
    GeminiOptions geminiOptions): IContentAnalysisService
{
    public async Task<AIResponseDto> GenerateAnalyzeAsync(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return new AIResponseDto
            {
                Summary = "İçerik boş.",
                Tags = new List<string> { "General" }
            };
        }

        var url =
            $"https://generativelanguage.googleapis.com/v1beta/models/{geminiOptions.Model}:generateContent?key={geminiOptions.ApiKey}";

        var systemInstruction =
            "Sen bir içerik analiz asistanısın. " +
            "ÇIKTIYI SADECE JSON olarak ver. " +
            "Şu şemayı kullan: {\"summary\":\"\",\"tags\":[]} " +
            "summary 6-7 cümle, tags en fazla 5 adet ve Türkçe olsun.";

        var requestBody = new
        {
            systemInstruction = new
            {
                parts = new[]
                {
                    new { text = systemInstruction }
                }
            },
            contents = new[]
            {
                new
                {
                    role = "user",
                    parts = new[]
                    {
                        new { text = text }
                    }
                }
            }
        };

        var response = await httpClient.PostAsJsonAsync(url, requestBody);
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();

        using var doc = JsonDocument.Parse(json);

        var aiText =
            doc.RootElement
               .GetProperty("candidates")[0]
               .GetProperty("content")
               .GetProperty("parts")[0]
               .GetProperty("text")
               .GetString();

        return JsonSerializer.Deserialize<AIResponseDto>(
            aiText!,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;
    }
}

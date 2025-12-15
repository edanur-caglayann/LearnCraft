using LearnCraftt.Application.Repositories.AIAnswerRepositories;
using LearnCraftt.Application.Repositories.AIFormatRepositories;
using LearnCraftt.Application.Repositories.AIGeneratedContentRepositories;
using LearnCraftt.Application.Repositories.AIQuestionRepositories;
using LearnCraftt.Application.Repositories.EmailConfirmationTokenRepositories;
using LearnCraftt.Application.Repositories.ExamRepositories;
using LearnCraftt.Application.Repositories.PasswordResetTokenRepositories;
using LearnCraftt.Application.Repositories.UploadedContentRepositories;
using LearnCraftt.Application.Repositories.UserAnswerRepositories;
using LearnCraftt.Application.Repositories.UserRepositories;
using LearnCraftt.Application.Repositories.UserScoreRepositories;
using LearnCraftt.Application.Services;
using LearnCraftt.Application.Services.Authentication;
using LearnCraftt.Persistence.Repositories.AIAnswerRepositories;
using LearnCraftt.Persistence.Repositories.AIFormatRepositories;
using LearnCraftt.Persistence.Repositories.AIGeneratedContentRepositories;
using LearnCraftt.Persistence.Repositories.AIQuestionRepositories;
using LearnCraftt.Persistence.Repositories.EmailConfirmationTokenRepositories;
using LearnCraftt.Persistence.Repositories.ExamRepositories;
using LearnCraftt.Persistence.Repositories.PasswordResetTokensRepositories;
using LearnCraftt.Persistence.Repositories.UploadedContentRepositories;
using LearnCraftt.Persistence.Repositories.UserAnswerRepositories;
using LearnCraftt.Persistence.Repositories.UserRepositories;
using LearnCraftt.Persistence.Repositories.UserScoreRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LearnCraftt.Persistence
{
    // Hangi interface’in hangi sınıfla çalışacağını .NET’e söylemek zorundayız.
    // Bunu ServiceRegistration’da yapıyoruz.
    public static class ServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<LearnCrafttDbContext>(opt =>
                opt.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            // AIAnswer
            services.AddScoped<IAIAnswerWriteRepository, AIAnswerWriteRepository>();
            services.AddScoped<IAIAnswerReadRepository, AIAnswerReadRepository>();
            //services.AddScoped<AIAnswerService>();

            // AIFormat
            services.AddScoped<IAIFormatWriteRepository, AIFormatWriteRepository>();
            services.AddScoped<IAIFormatReadRepository, AIFormatReadRepository>();
            //services.AddScoped<AIFormatService>();

            // AIGeneratedContent
            services.AddScoped<IAIGeneratedContentWriteRepository, AIGeneratedContentWriteRepository>();
            services.AddScoped<IAIGeneratedContentReadRepository, AIGeneratedContentReadRepository>();
            //services.AddScoped<AIGeneratedContentService>();

            // AIQuestion
            services.AddScoped<IAIQuestionWriteRepository, AIQuestionWriteRepository>();
            services.AddScoped<IAIQuestionReadRepository, AIQuestionReadRepository>();
            //services.AddScoped<AIQuestionService>();

            // Exam
            services.AddScoped<IExamWriteRepository, ExamWriteRepository>();
            services.AddScoped<IExamReadRepository, ExamReadRepository>();
            //services.AddScoped<ExamService>();

            // UploadedContent
            services.AddScoped<IContentWriteRepository, ContentWriteRepository>();
            services.AddScoped<IContentReadRepository, ContentReadRepository>();
            //services.AddScoped<UploadedContentService>();

            // UserAnswer
            services.AddScoped<IUserAnswerWriteRepository, UserAnswerWriteRepository>();
            services.AddScoped<IUserAnswerReadRepository, UserAnswerReadRepository>();
            //services.AddScoped<UserAnswerService>();

            // UserScore
            services.AddScoped<IUserScoreWriteRepository, UserScoreWriteRepository>();
            services.AddScoped<IUserScoreReadRepository, UserScoreReadRepository>();
            //services.AddScoped<UserScoreService>();

            // User
            services.AddScoped<IUserWriteRepository, UserWriteRepository>();
            services.AddScoped<IUserReadRepository, UserReadRepository>();
            services.AddScoped<UserService>();

            // DbContext
            services.AddDbContext<LearnCrafttDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            //PasswordResetToken
            services.AddScoped<IPasswordResetTokenReadRepository, PasswordResetTokenReadRepository>();
            services.AddScoped<IPasswordResetTokenWriteRepository, PasswordResetTokenWriteRepository>();

            //EmailConfirmationToken
            services.AddScoped<IEmailConfirmationTokenWriteRepository, EmailConfirmationTokenWriteRepository>();
            services.AddScoped<IEmailConfirmationTokenReadRepository, EmailConfirmationTokenReadRepository>();

            services.AddDbContext<LearnCrafttDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            return services;
        }
    }
}
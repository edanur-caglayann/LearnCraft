using LearnCraftt.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace LearnCraftt.Persistence
{
    public class LearnCrafttDbContext : DbContext
    {
        public LearnCrafttDbContext(DbContextOptions<LearnCrafttDbContext> options)
            : base(options)
        {
        }

        public DbSet<AIAnswer> AIAnswers { get; set; }
        public DbSet<AIFormat> AIFormats { get; set; }
        public DbSet<AIGeneratedContent> AIGeneratedContents { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<AIQuestion> AIQuestions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UploadedContent> UploadedContents { get; set; }
        public DbSet<UserScore> UserScores { get; set; }
        public DbSet<UserAnswer> UserAnswers { get; set; }
    }
}

using LearnCraftt.Domain.Entities;
using LearnCraftt.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;


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
        
        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                if (entry.State == EntityState.Added)
                    entry.Entity.CreatedDate = DateTime.Now;
                
                else if (entry.State == EntityState.Modified)
                    entry.Entity.UpdatedDate = DateTime.UtcNow;
            }
            return base.SaveChanges();
        }
    }
    
}

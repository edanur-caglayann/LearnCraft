using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace LearnCraftt.Persistence
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<LearnCrafttDbContext>
    {
        public LearnCrafttDbContext CreateDbContext(string[] args)
        {
            // API projesinin yolunu bul
            var basePath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "..",
                "LearnCraftt.Api"
            );

            // appsettings.json'u API klasöründen oku
            IConfigurationRoot config = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json")
                .Build();

            // Connection string al
            var connectionString = config.GetConnectionString("DefaultConnection");

            // DbContextOptions oluştur
            var optionsBuilder = new DbContextOptionsBuilder<LearnCrafttDbContext>();
            optionsBuilder.UseNpgsql(connectionString);

            return new LearnCrafttDbContext(optionsBuilder.Options);
        }
    }
}


/* migration sirasinda appsetting.json okunmaz, program.cs ve DI calismaz.
 * miigration tamamen tasarim zamaninda calisir.Yani EF Core, migration oluştururken:
   Program.cs içindeki AddDbContext çağrısını görmez
   builder.Configuration’a erişemez
   appsettings.json’u yükleyemez
   
   appsettings.json → runtime içindir
   DesignTimeDbContextFactory → migration içindir
 */
 
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Data
{
    public class MercadoFacilDbContextFactory: IDesignTimeDbContextFactory<MercadoFacilDbContext>
    {
        public MercadoFacilDbContext CreateDbContext(string[] args)
        {
            var projectPath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"../../MercadoFacilAPI/MercadoFacilAPI"));
            var configuration = new ConfigurationBuilder()  
                .SetBasePath(projectPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            var builder = new DbContextOptionsBuilder<MercadoFacilDbContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            builder.UseSqlServer(connectionString);

            return new MercadoFacilDbContext(builder.Options);
        }
    }
}
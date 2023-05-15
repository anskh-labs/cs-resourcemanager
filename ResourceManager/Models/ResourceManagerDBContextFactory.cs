using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Options;
using ResourceManager.Settings;

namespace ResourceManager.Models
{
    public class ResourceManagerDBContextFactory : IDesignTimeDbContextFactory<ResourceManagerDBContext>
    {
        private readonly string dbConnectionString;
        public ResourceManagerDBContextFactory(IOptionsMonitor<DBSettings> options)
        {
            dbConnectionString = options.CurrentValue.Sqlite;
        }
        public ResourceManagerDBContext CreateDbContext(string[] args = null!)
        {
            var options = new DbContextOptionsBuilder<ResourceManagerDBContext>();
            options.EnableSensitiveDataLogging(true);
            options.UseSqlite(dbConnectionString);

            return new ResourceManagerDBContext(options.Options);
        }
    }
}

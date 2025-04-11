using Contactly.Data;
using Microsoft.EntityFrameworkCore;
namespace Contactly.Extensions
{
    public static class MigrationExtensions
    {
        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            using IServiceScope scope =  app.ApplicationServices.CreateScope();
            using ContactlyDbContext dbContext = scope.ServiceProvider.GetRequiredService<ContactlyDbContext>();
            dbContext.Database.Migrate();
        }
    }
}

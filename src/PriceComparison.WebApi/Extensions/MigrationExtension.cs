using Microsoft.EntityFrameworkCore;
using PriceComparison.Infrastructure.EntityFramework;

namespace PriceComparison.WebApi.Extensions;

public static class MigrationExtension
{
    public static void ApplyMigration(this WebApplication application)
    {
        using var scope = application.Services.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        context.Database.Migrate();
    }
}
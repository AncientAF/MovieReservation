using Microsoft.AspNetCore.Builder;

namespace CinemaService.Infrastructure.Data.Extensions;

public static class DatabaseExtensions
{
    public static async Task InitialiseDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        await context.Database.MigrateAsync();

        await SeedAsync(context);
    }

    private static async Task SeedAsync(ApplicationDbContext context)
    {
        await SeedCinemasAsync(context);
    }

    private static async Task SeedCinemasAsync(ApplicationDbContext context)
    {
        if (!await context.Cinemas.AnyAsync())
        {
            await context.Cinemas.AddRangeAsync(InitialData.Cinemas);
            await context.SaveChangesAsync();
        }
    }
}
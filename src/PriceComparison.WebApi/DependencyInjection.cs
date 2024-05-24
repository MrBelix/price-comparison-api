using PriceComparison.Application;
using PriceComparison.Infrastructure;
using PriceComparison.Infrastructure.EntityFramework;
using PriceComparison.Infrastructure.Identity;

namespace PriceComparison.WebApi;

public static class DependencyInjection
{
    public static IServiceCollection AddPriceComparisonServices(this IServiceCollection services, IConfiguration configuration)
    {
        var databaseConfiguration = configuration.GetRequiredSection("Database")
            .Get<DatabaseConfiguration>();

        if (databaseConfiguration is null)
        {
            throw new InvalidOperationException("Database configuration is missing");
        }

        services
            .AddApplication()
            .AddInfrastructure(databaseConfiguration);

        return services;
    }
}
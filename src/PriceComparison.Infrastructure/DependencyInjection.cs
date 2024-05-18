using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PriceComparison.Application.Products.Interfaces;
using PriceComparison.Infrastructure.EntityFramework;
using PriceComparison.Infrastructure.EntityFramework.Repositories;
using PriceComparison.Infrastructure.EntityFramework.Services;

namespace PriceComparison.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, DatabaseConfiguration databaseConfiguration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(databaseConfiguration.GetConnectionString());
        });

        // register scoped dependencies
        services.AddScoped<IProductRepository, EfProductRepository>();
        services.AddScoped<IProductService, EfProductService>();

        return services;
    }
}
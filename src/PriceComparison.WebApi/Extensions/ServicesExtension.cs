using PriceComparison.Application;
using PriceComparison.Infrastructure;

namespace PriceComparison.WebApi.Extensions;

public static class ServicesExtension
{
    public static IServiceCollection AddPriceComparisonServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication();
        services.AddAuthorization();

        services.AddApplication()
            .AddInfrastructure(configuration);

        return services;
    }

    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        return services;
    }
}
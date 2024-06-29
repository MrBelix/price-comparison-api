using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using PriceComparison.Application.Authentication.Interfaces;
using PriceComparison.Application.Products.Interfaces;
using PriceComparison.Application.Users.Interfaces;
using PriceComparison.Infrastructure.Authentication;
using PriceComparison.Infrastructure.EntityFramework;
using PriceComparison.Infrastructure.EntityFramework.Repositories;
using PriceComparison.Infrastructure.EntityFramework.Services;

namespace PriceComparison.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddPersistence(configuration)
            .AddIdentityServices(configuration);

        return services;
    }

    private static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtOptions>(configuration.GetSection(JwtOptions.ConfigurationSectionName));

        var jwtOptions = JwtOptions.FromConfiguration(configuration);
        var signingConfigurations = new SigningConfigurations(jwtOptions.Secret);

        services.AddSingleton(signingConfigurations);
        services.AddSingleton<IPasswordHasher, PasswordHasher>();
        services.AddScoped<ITokenManager, JwtTokenManager>();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(jwtBearerOptions =>
            {
                jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtOptions.Issuer,
                    ValidAudience = jwtOptions.Audience,
                    IssuerSigningKey = signingConfigurations.SecurityKey,
                    ClockSkew = TimeSpan.Zero
                };
            });

        return services;
    }

    private static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var databaseConfiguration = DatabaseConfiguration.FromConfiguration(configuration);

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(databaseConfiguration.GetConnectionString());
        });

        // register scoped dependencies
        services.AddScoped<IProductRepository, EfProductRepository>();
        services.AddScoped<IProductService, EfProductService>();
        services.AddScoped<IUserRepository, EfUserRepository>();

        return services;
    }
}
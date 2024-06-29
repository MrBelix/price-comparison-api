using JetBrains.Annotations;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols.Configuration;

namespace PriceComparison.Infrastructure.EntityFramework;

[UsedImplicitly]
public class DatabaseConfiguration
{
    public const string SettingsKey = "Database";

    public string Host { get; init; } = string.Empty;

    public int Port { get; init; }

    public string Name { get; init; } = string.Empty;

    public string User { get; init; } = string.Empty;

    public string Password { get; init; } = string.Empty;

    public string GetConnectionString()
    {
        return $"Host={Host};Port={Port};Database={Name};Username={User};Password={Password}";
    }

    public static DatabaseConfiguration FromConfiguration(IConfiguration configuration)
    {
        return configuration.GetSection(SettingsKey).Get<DatabaseConfiguration>()
               ?? throw new InvalidConfigurationException("Database configuration is missing");
    }
}
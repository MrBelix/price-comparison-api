using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols.Configuration;

namespace PriceComparison.Infrastructure.Authentication;

public record JwtOptions
{
    public const string ConfigurationSectionName = "Jwt";

    public required string Audience { get; init; }
    public required string Issuer { get; init; }
    public required long AccessTokenExpiration { get; init; }
    public required long RefreshTokenExpiration { get; init; }
    public required string Secret { get; init; }

    public static JwtOptions FromConfiguration(IConfiguration configuration)
    {
        var config = configuration.GetSection(ConfigurationSectionName).Get<JwtOptions>()
                     ?? throw new InvalidConfigurationException("Jwt configuration is missing");

        return config;
    }
}
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PriceComparison.Application.Authentication;
using PriceComparison.Application.Authentication.Interfaces;
using PriceComparison.Application.Users.Interfaces;
using PriceComparison.Domain.Users;

namespace PriceComparison.Infrastructure.Authentication;

public class JwtTokenManager(
    IOptions<JwtOptions> jwtOptions,
    SigningConfigurations signingConfigurations,
    IUserRepository userRepository)
    : ITokenManager
{
    private readonly JwtOptions _jwtOptions = jwtOptions.Value;

    public AccessToken CreateAccessToken(User user)
    {
        return new AccessToken(
            BuildAccessToken(user),
            BuildRefreshToken(user),
            DateTime.UtcNow.AddSeconds(_jwtOptions.AccessTokenExpiration));
    }

    public async Task<AccessToken?> RefreshAccessToken(string refreshToken)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true, // Here we are saying that we don't care about the token's expiration date
            ValidateIssuerSigningKey = true,
            ValidIssuer = _jwtOptions.Issuer,
            ValidAudience = _jwtOptions.Audience,
            IssuerSigningKey = signingConfigurations.SecurityKey
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(refreshToken, tokenValidationParameters, out var securityToken);

        if (securityToken is null || !Guid.TryParse(principal.FindFirstValue(ClaimTypes.NameIdentifier), out var guid))
            throw new SecurityTokenException("Invalid token");

        var user = await userRepository.GetByIdAsync(new UserId(guid));

        if (user is null)
            throw new SecurityTokenException("Invalid token");

        return CreateAccessToken(user);
    }

    private string BuildAccessToken(User user)
    {
        var accessTokenExpiration = DateTime.UtcNow.AddMinutes(_jwtOptions.AccessTokenExpiration);

        var securityToken = new JwtSecurityToken
        (
            issuer: _jwtOptions.Issuer,
            audience: _jwtOptions.Audience,
            claims: GetClaims(user),
            expires: accessTokenExpiration,
            notBefore: DateTime.UtcNow,
            signingCredentials: signingConfigurations.SigningCredentials
        );

        var handler = new JwtSecurityTokenHandler();
        return handler.WriteToken(securityToken);
    }

    private string BuildRefreshToken(User user)
    {
        var refreshTokenExpiration = DateTime.UtcNow.AddDays(_jwtOptions.RefreshTokenExpiration);

        List<Claim> claims =
        [
            new Claim(ClaimTypes.NameIdentifier, user.Id.Value.ToString()),
        ];

        var securityToken = new JwtSecurityToken
        (
            issuer: _jwtOptions.Issuer,
            audience: _jwtOptions.Audience,
            claims: claims,
            expires: refreshTokenExpiration,
            notBefore: DateTime.UtcNow,
            signingCredentials: signingConfigurations.SigningCredentials
        );

        var handler = new JwtSecurityTokenHandler();
        return handler.WriteToken(securityToken);
    }

    private static List<Claim> GetClaims(User user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Email, user.Email)
        };

        return claims;
    }
}
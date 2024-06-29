using PriceComparison.Domain.Users;

namespace PriceComparison.Application.Authentication.Interfaces;

public interface ITokenManager
{
    public AccessToken CreateAccessToken(User user);

    public Task<AccessToken?> RefreshAccessToken(string refreshToken);
}
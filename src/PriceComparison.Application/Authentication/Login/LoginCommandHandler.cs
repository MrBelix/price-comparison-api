using PriceComparison.Application.Authentication.Interfaces;
using PriceComparison.Application.Common.Interfaces;
using PriceComparison.Application.Users.Interfaces;
using PriceComparison.Contracts.Authentication;

namespace PriceComparison.Application.Authentication.Login;

public class LoginCommandHandler(
    ITokenManager tokenManager,
    IUserRepository userRepository,
    IPasswordHasher passwordHasher)
    : ICommandHandler<LoginCommand, AccessTokenResponse>
{
    public async Task<AccessTokenResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByEmail(request.Email);

        if (user is null || !passwordHasher.ValidatePassword(user.Password, request.Password))
        {
            throw new ArgumentException("Invalid credentials");
        }

        var token = tokenManager.CreateAccessToken(user);

        return new AccessTokenResponse(
            token.Access,
            token.Refresh,
            token.Expires);
    }
}
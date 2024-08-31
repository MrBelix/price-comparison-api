using FluentValidation;
using PriceComparison.Application.Authentication.Interfaces;
using PriceComparison.Application.Common.Interfaces;
using PriceComparison.Contracts.Authentication;

namespace PriceComparison.Application.Authentication.Refresh;

public class RefreshTokenCommandHandler(ITokenManager tokenManager)
    : ICommandHandler<RefreshTokenCommand, AccessTokenResponse>
{
    public async Task<AccessTokenResponse> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var token = await tokenManager.RefreshAccessToken(request.RefreshToken);

        if (token is null)
        {
            throw new ValidationException("Invalid refresh token");
        }

        return new AccessTokenResponse(
            token.Access,
            token.Refresh,
            token.Expires);
    }
}
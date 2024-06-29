using PriceComparison.Application.Common.Interfaces;
using PriceComparison.Contracts.Authentication;

namespace PriceComparison.Application.Authentication.Refresh;

public sealed record RefreshTokenCommand(string RefreshToken) : ICommand<AccessTokenResponse>;
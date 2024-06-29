using PriceComparison.Application.Common.Interfaces;
using PriceComparison.Contracts.Authentication;

namespace PriceComparison.Application.Authentication.Login;

public record LoginCommand(string Email, string Password) : ICommand<AccessTokenResponse>;
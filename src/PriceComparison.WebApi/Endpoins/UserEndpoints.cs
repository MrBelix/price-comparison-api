using MediatR;
using PriceComparison.Application.Authentication.Login;
using PriceComparison.Application.Authentication.Refresh;
using PriceComparison.Application.Users.Create;
using PriceComparison.Contracts.Authentication;
using PriceComparison.Contracts.Users;

namespace PriceComparison.WebApi.Endpoins;

public static class UserEndpoints
{
    public static IEndpointRouteBuilder AddUserEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/register", (CreateUserRequest request, ISender sender) =>
            sender.Send(new CreateUserCommand(request.Email, request.Password)));

        endpoints.MapPost("/login", (LoginRequest request, ISender sender) =>
            sender.Send(new LoginCommand(request.Email, request.Password)));

        endpoints.MapPost("/refresh", (RefreshTokenRequest request, ISender sender) =>
            sender.Send(new RefreshTokenCommand(request.RefreshToken)));

        return endpoints;
    }
}
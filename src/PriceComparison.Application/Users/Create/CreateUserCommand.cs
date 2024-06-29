using PriceComparison.Application.Common.Interfaces;

namespace PriceComparison.Application.Users.Create;

public record CreateUserCommand(string Email, string Password) : ICommand;
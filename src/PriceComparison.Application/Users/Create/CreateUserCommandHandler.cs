using System.ComponentModel.DataAnnotations;
using PriceComparison.Application.Authentication.Interfaces;
using PriceComparison.Application.Common.Interfaces;
using PriceComparison.Application.Users.Interfaces;
using PriceComparison.Domain.Users;

namespace PriceComparison.Application.Users.Create;

public class CreateUserCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher) : ICommandHandler<CreateUserCommand>
{
    public async Task Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        if (await userRepository.GetByEmail(request.Email) is not null)
        {
            throw new ValidationException("Email already taken");
        }

        var user = User.Create(request.Email, passwordHasher.HashPassword(request.Password));

        await userRepository.AddAsync(user);
    }
}
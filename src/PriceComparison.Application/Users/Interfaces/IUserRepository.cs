using PriceComparison.Application.Common.Interfaces;
using PriceComparison.Domain.Users;

namespace PriceComparison.Application.Users.Interfaces;

public interface IUserRepository : IRepository<User, UserId>
{
    Task<User?> GetByEmail(string email);
}
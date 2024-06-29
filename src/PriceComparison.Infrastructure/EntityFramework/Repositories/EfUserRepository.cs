using Microsoft.EntityFrameworkCore;
using PriceComparison.Application.Users.Interfaces;
using PriceComparison.Domain.Users;

namespace PriceComparison.Infrastructure.EntityFramework.Repositories;

public class EfUserRepository(ApplicationDbContext context)
    : EfRepository<User, UserId>(context), IUserRepository
{
    public Task<User?> GetByEmail(string email)
    {
        return DbSet.Where(e => e.Email.Contains(email)).FirstOrDefaultAsync();
    }
}
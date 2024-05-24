using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PriceComparison.Domain.Products;
using PriceComparison.Domain.Users;
using PriceComparison.Infrastructure.Identity;

namespace PriceComparison.Infrastructure.EntityFramework;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser, ApplicationRole, UserId>(options)
{
    public required DbSet<Product> Products { get; init; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DependencyInjection).Assembly);
    }
}
using Microsoft.EntityFrameworkCore;
using PriceComparison.Domain.Products;

namespace PriceComparison.Infrastructure.EntityFramework;

public class ApplicationDbContext(DbContextOptions options) : DbContext(options)
{
    public required DbSet<Product> Products { get; init; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DependencyInjection).Assembly);
    }
}
using PriceComparison.Application.Products.Interfaces;
using PriceComparison.Domain.Products;

namespace PriceComparison.Infrastructure.EntityFramework.Repositories;

public class EfProductRepository(ApplicationDbContext context)
    : EfRepository<Product, ProductId>(context), IProductRepository
{

}
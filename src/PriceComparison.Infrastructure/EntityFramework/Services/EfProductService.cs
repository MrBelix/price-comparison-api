using Microsoft.EntityFrameworkCore;
using PriceComparison.Application.Products.Interfaces;
using PriceComparison.Contracts.Products;

namespace PriceComparison.Infrastructure.EntityFramework.Services;

public class EfProductService(ApplicationDbContext context) : IProductService
{
    public Task<ProductResponse?> GetByIdAsync(Guid id)
    {
        return context.Products.Select(x =>
                new ProductResponse(
                    x.Id.Value,
                    x.Name,
                    x.Description,
                    x.Prices.Select(price =>
                            new ProductPriceResponse(
                                price.MerchantId.Value,
                                price.Price))
                        .ToList()))
            .FirstOrDefaultAsync();
    }

    public async Task<IList<ProductResponse>> GetAllAsync()
    {
        return await context.Products.Select(x =>
                new ProductResponse(
                    x.Id.Value,
                    x.Name,
                    x.Description,
                    x.Prices.Select(price =>
                            new ProductPriceResponse(
                                price.MerchantId.Value,
                                price.Price))
                        .ToList()))
            .ToListAsync();
    }
}
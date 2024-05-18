using PriceComparison.Contracts.Products;

namespace PriceComparison.Application.Products.Interfaces;

public interface IProductService
{
    Task<ProductResponse?> GetByIdAsync(Guid id);

    Task<IList<ProductResponse>> GetAllAsync();
}
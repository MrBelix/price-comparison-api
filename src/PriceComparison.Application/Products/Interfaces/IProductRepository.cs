using PriceComparison.Application.Common.Interfaces;
using PriceComparison.Domain.Products;

namespace PriceComparison.Application.Products.Interfaces;

public interface IProductRepository : IRepository<Product, ProductId>
{

}
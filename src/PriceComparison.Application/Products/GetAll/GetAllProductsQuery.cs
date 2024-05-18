using PriceComparison.Application.Common.Interfaces;
using PriceComparison.Contracts.Products;

namespace PriceComparison.Application.Products.GetAll;

public record GetAllProductsQuery : IQuery<IList<ProductResponse>>;
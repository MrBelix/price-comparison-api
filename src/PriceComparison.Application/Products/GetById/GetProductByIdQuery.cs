using PriceComparison.Application.Common.Interfaces;
using PriceComparison.Contracts.Products;

namespace PriceComparison.Application.Products.GetById;

public record GetProductByIdQuery(Guid ProductId) : IQuery<ProductResponse>;
using PriceComparison.Application.Common.Interfaces;
using PriceComparison.Domain.Products;

namespace PriceComparison.Application.Products.Delete;

public record DeleteProductCommand(ProductId ProductId) : ICommand;
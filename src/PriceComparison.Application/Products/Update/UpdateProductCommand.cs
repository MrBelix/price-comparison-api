using PriceComparison.Application.Common.Interfaces;
using PriceComparison.Domain.Common;
using PriceComparison.Domain.Merchants;
using PriceComparison.Domain.Products;

namespace PriceComparison.Application.Products.Update;

public record UpdateProductCommand(
    ProductId ProductId,
    Slug Slug,
    string Name,
    string Description,
    IDictionary<MerchantId, double> Prices) : ICommand;
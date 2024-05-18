using PriceComparison.Application.Common.Interfaces;
using PriceComparison.Application.Products.UpsertPrice;
using PriceComparison.Domain.Common;
using PriceComparison.Domain.Merchants;
using PriceComparison.Domain.Products;

namespace PriceComparison.Application.Products.Create;

public record CreateProductCommand(
    Slug Slug,
    string Name,
    string Description,
    IDictionary<MerchantId, double> Prices) : ICommand;
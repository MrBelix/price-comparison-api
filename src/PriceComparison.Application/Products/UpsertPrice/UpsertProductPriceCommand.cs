using PriceComparison.Domain.Merchants;
using PriceComparison.Domain.Products;
using ICommand = PriceComparison.Application.Common.Interfaces.ICommand;

namespace PriceComparison.Application.Products.UpsertPrice;

public record UpsertProductPriceCommand(
    ProductId ProductId,
    MerchantId MerchantId,
    double Price) : ICommand;
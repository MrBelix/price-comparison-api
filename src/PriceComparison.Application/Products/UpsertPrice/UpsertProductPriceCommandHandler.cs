using PriceComparison.Application.Common.Interfaces;
using PriceComparison.Application.Products.Interfaces;

namespace PriceComparison.Application.Products.UpsertPrice;

public class UpsertProductPriceCommandHandler(IProductRepository productRepository) : ICommandHandler<UpsertProductPriceCommand>
{

    public async Task Handle(UpsertProductPriceCommand request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetByIdAsync(request.ProductId);

        if (product is null)
        {
            throw new NullReferenceException(nameof(product));
        }

        product.CreateOrUpdatePrice(request.MerchantId, request.Price);
        await productRepository.UpdateAsync(product);
    }
}
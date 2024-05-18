using PriceComparison.Application.Common.Interfaces;
using PriceComparison.Application.Products.Interfaces;

namespace PriceComparison.Application.Products.Update;

public class UpdateProductCommandHandler(IProductRepository productRepository) : ICommandHandler<UpdateProductCommand>
{
    public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetByIdAsync(request.ProductId);

        if (product is null)
        {
            throw new NullReferenceException(nameof(product));
        }

        product.SetSlug(request.Slug);
        product.SetName(request.Name);
        product.SetDescription(request.Description);

        foreach (var (merchantId, price) in request.Prices)
        {
            product.CreateOrUpdatePrice(merchantId, price);
        }

        await productRepository.UpdateAsync(product);
    }
}
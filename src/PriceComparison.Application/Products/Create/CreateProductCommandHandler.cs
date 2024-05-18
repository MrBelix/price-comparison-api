using PriceComparison.Application.Common.Interfaces;
using PriceComparison.Application.Products.Interfaces;
using PriceComparison.Domain.Products;

namespace PriceComparison.Application.Products.Create;

public class CreateProductCommandHandler(IProductRepository productRepository) : ICommandHandler<CreateProductCommand>
{
    public async Task Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = Product.Create(request.Slug, request.Name, request.Description);

        foreach (var (merchantId, price) in request.Prices)
        {
            product.CreateOrUpdatePrice(merchantId, price);
        }

        await productRepository.AddAsync(product);
    }
}
using PriceComparison.Application.Common.Interfaces;
using PriceComparison.Application.Products.Interfaces;

namespace PriceComparison.Application.Products.Delete;

public class DeleteProductCommandHandler(IProductRepository productRepository) : ICommandHandler<DeleteProductCommand>
{
    public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetByIdAsync(request.ProductId);

        if (product is null)
        {
            throw new NullReferenceException(nameof(product));
        }

        await productRepository.DeleteAsync(product);
    }
}
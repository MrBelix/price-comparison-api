using PriceComparison.Application.Common.Interfaces;
using PriceComparison.Application.Products.Interfaces;
using PriceComparison.Contracts.Products;

namespace PriceComparison.Application.Products.GetById;

public class GetProductByIdQueryHandler(IProductService productService) : IQueryHandler<GetProductByIdQuery, ProductResponse>
{
    public async Task<ProductResponse> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await productService.GetByIdAsync(request.ProductId);
        if (product is null)
        {
            throw new NullReferenceException(nameof(product));
        }

        return product;
    }
}
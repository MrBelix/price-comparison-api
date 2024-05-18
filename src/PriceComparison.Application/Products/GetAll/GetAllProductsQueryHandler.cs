using PriceComparison.Application.Common.Interfaces;
using PriceComparison.Application.Products.Interfaces;
using PriceComparison.Contracts.Products;

namespace PriceComparison.Application.Products.GetAll;

public class GetAllProductsQueryHandler(IProductService productService) : IQueryHandler<GetAllProductsQuery, IList<ProductResponse>>
{
    public async Task<IList<ProductResponse>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var result = await productService.GetAllAsync();

        return result;
    }
}
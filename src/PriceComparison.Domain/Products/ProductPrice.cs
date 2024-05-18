using JetBrains.Annotations;
using PriceComparison.Domain.Common;
using PriceComparison.Domain.Merchants;

namespace PriceComparison.Domain.Products;

public class ProductPrice: Entity<ProductPriceId>
{
    [UsedImplicitly]
    public MerchantId MerchantId { get; init; }

    [UsedImplicitly]
    public double Price { get; private set; }

    private ProductPrice(ProductPriceId id, MerchantId merchantId, double price)
        : base(id)
    {
        MerchantId = merchantId;
        Price = price;
    }

    private ProductPrice()
        : base(null!)
    {
    }

    public static ProductPrice Create(MerchantId merchantId, double price)
    {
        return new ProductPrice(
            ProductPriceId.New,
            merchantId,
            price);
    }

    public void UpdatePrice(double price)
    {
        Price = price;
    }

}
using JetBrains.Annotations;
using PriceComparison.Domain.Common;
using PriceComparison.Domain.Merchants;

namespace PriceComparison.Domain.Products;

public class Product : Entity<ProductId>
{
    private readonly List<ProductPrice> _prices = [];

    [UsedImplicitly]
    public Slug Slug { get; private set; }

    [UsedImplicitly]
    public string Name { get; private set; }

    [UsedImplicitly]
    public string Description { get; private set; }

    [UsedImplicitly]
    public IReadOnlyCollection<ProductPrice> Prices => _prices
        .AsReadOnly();

    private Product(ProductId id, Slug slug, string name, string description)
        : base(id)
    {
        Slug = slug;
        Name = name;
        Description = description;
    }

    private Product()
        : base(ProductId.Empty)
    {

    }

    public static Product Create(Slug slug, string name, string description)
    {
        return new Product(
            ProductId.New,
            slug,
            name,
            description);
    }

    public void CreateOrUpdatePrice(MerchantId merchantId, double price)
    {
        var existedPrice = _prices
            .FirstOrDefault(x => x.MerchantId == merchantId);

        if (existedPrice is not null)
        {
            existedPrice.UpdatePrice(price);
            return;
        }

        _prices.Add(ProductPrice.Create(merchantId, price));
    }

    public void SetName(string name)
    {
        Name = name;
    }

    public void SetDescription(string description)
    {
        Description = description;
    }

    public void SetSlug(Slug slug)
    {
        Slug = slug;
    }
}
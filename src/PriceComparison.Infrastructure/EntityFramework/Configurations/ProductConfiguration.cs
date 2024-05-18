using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PriceComparison.Domain.Merchants;
using PriceComparison.Domain.Products;
using PriceComparison.Infrastructure.EntityFramework.Converters;

namespace PriceComparison.Infrastructure.EntityFramework.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");

        builder
            .Property(e => e.Id)
            .HasConversion<StrongGuidIdConverter<ProductId>>();

        builder
            .Property(e => e.Slug)
            .HasConversion<SlugConverter>();

        builder
            .OwnsMany(e => e.Prices, mb =>
            {
                mb.ToTable("ProductPrices");

                mb.Property(e => e.Id)
                    .HasConversion<StrongGuidIdConverter<ProductPriceId>>();

                mb.Property(e => e.MerchantId)
                    .HasConversion<StrongGuidIdConverter<MerchantId>>();
            });
    }
}
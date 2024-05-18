using PriceComparison.Domain.Common;

namespace PriceComparison.Domain.Products;

public record ProductPriceId(Guid Value) : StrongGuidId<ProductPriceId>(Value);
using PriceComparison.Domain.Common;

namespace PriceComparison.Domain.Products;

public record ProductId(Guid Value) : StrongGuidId<ProductId>(Value);
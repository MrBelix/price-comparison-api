using PriceComparison.Domain.Common;

namespace PriceComparison.Domain.Merchants;

public record MerchantId(Guid Value) : StrongGuidId<MerchantId>(Value);
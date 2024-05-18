using PriceComparison.Domain.Common;

namespace PriceComparison.Domain.Users;

public record UserId(Guid Value) : StrongGuidId<UserId>(Value);
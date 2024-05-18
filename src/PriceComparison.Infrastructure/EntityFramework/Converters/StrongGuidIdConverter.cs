using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PriceComparison.Domain.Common;

namespace PriceComparison.Infrastructure.EntityFramework.Converters;

public class StrongGuidIdConverter<T>()
    : ValueConverter<T, Guid>(
        id => id.Value,
        val => (T)Activator.CreateInstance(typeof(T), val)!)
    where T : StrongGuidId<T>;
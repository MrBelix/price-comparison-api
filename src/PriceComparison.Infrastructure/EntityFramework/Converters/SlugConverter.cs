using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PriceComparison.Domain.Common;

namespace PriceComparison.Infrastructure.EntityFramework.Converters;

public class SlugConverter()
    : ValueConverter<Slug, string>(
        v => v.Value,
        v => new Slug(v));
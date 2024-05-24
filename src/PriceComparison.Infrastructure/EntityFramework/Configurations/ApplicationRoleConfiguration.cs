using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PriceComparison.Domain.Users;
using PriceComparison.Infrastructure.EntityFramework.Converters;

namespace PriceComparison.Infrastructure.EntityFramework.Configurations;

public class ApplicationRoleConfiguration : IEntityTypeConfiguration<ApplicationRole>
{
    public void Configure(EntityTypeBuilder<ApplicationRole> builder)
    {
        builder
            .Property(e => e.Id)
            .HasConversion<StrongGuidIdConverter<UserId>>()
            .ValueGeneratedOnAdd();;
    }
}
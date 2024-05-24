using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PriceComparison.Domain.Users;
using PriceComparison.Infrastructure.EntityFramework.Converters;
using PriceComparison.Infrastructure.Identity;

namespace PriceComparison.Infrastructure.EntityFramework.Configurations;

public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder
            .Property(e => e.Id)
            .HasConversion<StrongGuidIdConverter<UserId>>()
            .ValueGeneratedOnAdd();
    }
}
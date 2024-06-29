using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PriceComparison.Domain.Users;
using PriceComparison.Infrastructure.EntityFramework.Converters;

namespace PriceComparison.Infrastructure.EntityFramework.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder
            .Property(e => e.Id)
            .HasConversion<StrongGuidIdConverter<UserId>>()
            .ValueGeneratedOnAdd();
    }
}
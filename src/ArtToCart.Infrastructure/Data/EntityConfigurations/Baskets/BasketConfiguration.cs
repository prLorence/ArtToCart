using ArtToCart.Domain.Baskets;
using ArtToCart.Domain.Baskets.ValueObjects;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArtToCart.Infrastructure.Data.EntityConfigurations.Baskets;

public class BasketConfiguration : IEntityTypeConfiguration<Basket>
{
    public void Configure(EntityTypeBuilder<Domain.Baskets.Basket> builder)
    {
        var navigation = builder.Metadata.FindNavigation(nameof(Basket.Items));
        navigation?.SetPropertyAccessMode(PropertyAccessMode.Field);

        builder.HasKey(b => b.Id);

        builder.Property(b => b.Id)
            .HasConversion(id => id.Value,
                value => BasketId.CreateFrom((value)));

        builder.Property(b => b.BuyerId)
            .IsRequired()
            .HasMaxLength(256);
    }
}
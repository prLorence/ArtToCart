using ArtToCart.Domain.Baskets;
using ArtToCart.Domain.Baskets.ValueObjects;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArtToCart.Infrastructure.Data.EntityConfigurations.Baskets;

public class BasketItemConfiguration : IEntityTypeConfiguration<BasketItem>
{
    public void Configure(EntityTypeBuilder<BasketItem> builder)
    {
        builder.HasKey(bi => bi.Id);

        builder.Property(bi => bi.Id)
            .HasConversion(id => id.Value,
                value => BasketItemId.CreateFrom(value));

        builder.Property(bi => bi.BasketId)
            .HasConversion(id => id.Value,
                value => BasketId.CreateFrom(value));

        builder.Property(bi => bi.UnitPrice)
            .IsRequired(true)
            .HasColumnType("decimal(18,2)");

        builder.HasOne(bi => bi.Basket)
            .WithMany(b => b.Items)
            .HasForeignKey(bi => bi.BasketId);
    }
}
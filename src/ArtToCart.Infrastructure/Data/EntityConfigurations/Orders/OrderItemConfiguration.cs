using ArtToCart.Domain.Orders;
using ArtToCart.Domain.Orders.ValueObjects;
using ArtToCart.Domain.Products.ValueObjects;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArtToCart.Infrastructure.Data.EntityConfigurations.Orders;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.HasKey(oi => oi.Id);

        builder.Property(oi => oi.Id)
            .HasConversion(id => id.Value,
                value => OrderItemId.CreateFrom(value));

        builder.OwnsOne(i => i.ItemOrdered, io =>
        {
            io.WithOwner();

            io.Property(io => io.CatalogItemId)
                .HasConversion(id => id.Value,
                    value => CatalogItemId.CreateFrom(value));

            io.Property(cio => cio.ProductName)
                .HasMaxLength(50)
                .IsRequired();
        });

        builder.Property(oi => oi.UnitPrice)
            .IsRequired(true)
            .HasColumnType("decimal(18,2)");
    }
}
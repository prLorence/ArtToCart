using ArtToCart.Domain.Products;
using ArtToCart.Domain.Products.ValueObjects;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArtToCart.Infrastructure.Data.EntityConfigurations.Products;

public class CatalogItemConfiguration : IEntityTypeConfiguration<CatalogItem>
{
    public void Configure(EntityTypeBuilder<CatalogItem> builder)
    {
        builder.HasKey(ci => ci.Id);

        builder.Property(ci => ci.Id)
            .HasConversion(id => id.Value,
                value => CatalogItemId.CreateFrom(value));

        builder.Property(ci => ci.Name).HasMaxLength(50);

        builder.Property(ci => ci.Price);

        builder.Property(ci => ci.Size);

        builder.Property(ci => ci.Description).HasMaxLength(100);

        builder.Property(ci => ci.SellerId);

        builder.Property(ci => ci.CatalogTypeId)
            .HasConversion(id => id.Value,
                value => CatalogTypeId.CreateFrom(value));

        builder.HasMany(ci => ci.Images)
            .WithOne(i => i.CatalogItem)
            .HasForeignKey(x => x.CatalogItemId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(ci => ci.Reviews)
            .WithOne(i => i.CatalogItem)
            .HasForeignKey(x => x.CatalogItemId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(ci => ci.CatalogType)
            .WithMany()
            .HasForeignKey(ci => ci.CatalogTypeId);

        builder.OwnsOne(ci => ci.AverageRating);

        builder.Metadata
            .FindNavigation(nameof(CatalogItem.Images))
            .SetPropertyAccessMode(PropertyAccessMode.Field);

        builder.Metadata
            .FindNavigation(nameof(CatalogItem.Reviews))
            .SetPropertyAccessMode(PropertyAccessMode.Field);

    }
}
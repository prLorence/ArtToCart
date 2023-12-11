using ArtToCart.Domain.Products.Entities;
using ArtToCart.Domain.Products.ValueObjects;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArtToCart.Infrastructure.Data.EntityConfigurations.Products;

public class ProductImageConfiguration : IEntityTypeConfiguration<ProductImage>
{
    public void Configure(EntityTypeBuilder<ProductImage> builder)
    {
        builder.HasKey(c => c.Id);

        builder.HasIndex(x => x.Id).IsUnique();

        builder.Property(x => x.Id)
            .HasConversion(id => id.Value,
                value=> ProductImageId.CreateFrom(value))
            .ValueGeneratedNever();
    }
}
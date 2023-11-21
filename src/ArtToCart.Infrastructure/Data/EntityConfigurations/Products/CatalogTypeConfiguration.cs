using ArtToCart.Domain.Products;
using ArtToCart.Domain.Products.ValueObjects;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArtToCart.Infrastructure.Data.EntityConfigurations.Products;

public class CatalogTypeConfiguration : IEntityTypeConfiguration<CatalogType>
{
    public void Configure(EntityTypeBuilder<CatalogType> builder)
    {
        builder.ToTable("catalog_type");

        builder.HasKey(ct => ct.Id);

        builder.Property(ct => ct.Id)
            .HasConversion(id => id.Value,
                value => CatalogTypeId.CreateFrom(value));

        builder.Property(ct => ct.Type)
            .HasMaxLength(100)
            .IsRequired();
    }
}
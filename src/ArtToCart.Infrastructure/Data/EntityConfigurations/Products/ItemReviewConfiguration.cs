using ArtToCart.Domain.Products.Entities;
using ArtToCart.Domain.Products.ValueObjects;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArtToCart.Infrastructure.Data.EntityConfigurations.Products;

public class ItemReviewConfiguration : IEntityTypeConfiguration<ItemReview>
{
    public void Configure(EntityTypeBuilder<ItemReview> builder)
    {
        builder.HasKey(c => c.Id);

        builder.HasIndex(x => x.Id).IsUnique();

        builder.Property(x => x.Id)
            .HasConversion(id => id.Value,
                value=> ItemReviewId.CreateFrom(value))
            .ValueGeneratedNever();

        builder.Property(x => x.Value)
            .HasMaxLength(200)
            .IsRequired(true);

        builder.Property(x => x.BuyerId);
    }
}
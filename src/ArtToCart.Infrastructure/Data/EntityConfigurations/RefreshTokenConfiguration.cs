using ArtToCart.Application.Shared.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArtToCart.Infrastructure.Data.EntityConfigurations;

public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.ToTable("RefreshTokens");

        builder.Property<Guid>("Id")
            .ValueGeneratedOnAdd();

        builder.HasKey("Id");

        builder.HasIndex(rt => new { rt.Token, rt.UserId }).IsUnique();

        builder.HasOne(rt => rt.ApplicationUser)
            .WithMany(au => au.RefreshTokens)
            .HasForeignKey(x => x.UserId);

        builder.Property(rt => rt.Token).HasMaxLength(100);
        builder.Property(rt => rt.CreatedAt);
        builder.Ignore(rt => rt.IsActive);
        builder.Ignore(rt => rt.IsExpired);
    }
}
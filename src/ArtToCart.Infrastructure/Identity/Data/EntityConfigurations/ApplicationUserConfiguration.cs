using ArtToCart.Modules.Identity.Shared.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArtToCart.Infrastructure.Identity.Data.EntityConfigurations;

public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.Property(u => u.FirstName).HasMaxLength(100).IsRequired();
        builder.Property(u => u.LastName).HasMaxLength(100).IsRequired();
        builder.Property(u => u.UserName).HasMaxLength(50).IsRequired();
        builder.Property(u => u.NormalizedUserName).HasMaxLength(50).IsRequired();
        builder.Property(u => u.Email).HasMaxLength(50).IsRequired();
        builder.Property(u => u.NormalizedEmail).HasMaxLength(50).IsRequired();

        builder.Property(u => u.CreatedAt).HasDefaultValueSql("now()");

        builder.HasIndex(u => u.Email).IsUnique();
        builder.HasIndex(u => u.NormalizedEmail).IsUnique();
    }
}
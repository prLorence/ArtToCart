using ArtToCart.Application.Shared.Models;
using ArtToCart.Infrastructure.Data;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ArtToCart.Infrastructure.Shared;

public class ArtToCartIdentityDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
{
    public ArtToCartIdentityDbContext(DbContextOptions options) : base(options)
    {
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ArtToCartIdentityDbContext).Assembly);

        modelBuilder.Model.GetEntityTypes()
          .SelectMany(entity => entity.GetProperties())
          .Where(p => p.IsPrimaryKey())
          .ToList()
          .ForEach(p => p.ValueGenerated = ValueGenerated.Never);

    }


    public DbSet<ApplicationUser> Users { get; set; }
}
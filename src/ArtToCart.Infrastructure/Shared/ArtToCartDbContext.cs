using ArtToCart.Infrastructure.Identity.Models;
using ArtToCart.Modules.Identity.Shared.Models;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ArtToCart.Infrastructure.Shared;

public class ArtToCartDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
{
    public ArtToCartDbContext(DbContextOptions options) : base(options)
    {
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ArtToCartDbContext).Assembly);

        modelBuilder.Model.GetEntityTypes()
          .SelectMany(entity => entity.GetProperties())
          .Where(p => p.IsPrimaryKey())
          .ToList()
          .ForEach(p => p.ValueGenerated = ValueGenerated.Never);
    }

    public DbSet<ApplicationUser> Users { get; set; }
}
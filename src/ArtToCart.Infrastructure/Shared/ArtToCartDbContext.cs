using ArtToCart.Application.Shared.Interfaces;
using ArtToCart.Application.Shared.Models;
using ArtToCart.Domain.Baskets;
using ArtToCart.Domain.Orders;
using ArtToCart.Domain.Products;
using ArtToCart.Domain.Products.Entities;
using ArtToCart.Infrastructure.Data;

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

    public DbSet<CatalogType> CatalogTypes { get; set; }
    public DbSet<ProductImage> ProductImages { get; set; }
    public DbSet<ItemReview> ItemReview { get; set; }
    public DbSet<CatalogItem> CatalogItems { get; set; }
    public DbSet<ApplicationUser> Users { get; set; }

    public DbSet<Basket> Basket { get; set; }
    public DbSet<Order> Order { get; set; }
}
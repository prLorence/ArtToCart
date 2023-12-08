using ArtToCart.Domain.Products;
using ArtToCart.Domain.Products.Entities;
using ArtToCart.Domain.Products.ValueObjects;
using ArtToCart.Infrastructure.Shared;
using ArtToCart.Infrastructure.Shared.Persistance;

using Microsoft.EntityFrameworkCore;

namespace ArtToCart.Infrastructure.Data;

public class CatalogDataSeeder : IDataSeeder
{
    private readonly ArtToCartIdentityDbContext _context;

    public CatalogDataSeeder(ArtToCartIdentityDbContext context)
    {
        _context = context;
    }
    public async Task SeedAllAsync()
    {
        await SeedProducts();
    }

    public async Task SeedProducts()
    {
        CatalogType catalogType = CatalogType.Create("Test Catalog");

        CatalogItem catalogItems = CatalogItem.Create(
            "Test Product",
            1000.00m,
            "Test Description",
            "L",
            "sellerId",
            catalogType.Id,
                new List<ProductImage>());

        catalogItems.AddProductImages( new List<ProductImage> {
            new(
            ProductImageId.CreateUnique(),
            "https://sparkbikereview.com/wp-content/uploads/2019/08/P6A3895-min.jpg",
            true,
            catalogItems.Id)
        });

        if (await _context.CatalogItems.FirstOrDefaultAsync(ci => ci.Name == catalogItems.Name) == null)
        {
            await _context.AddAsync(catalogType);

            await _context.AddAsync(catalogItems);

            await _context.SaveChangesAsync();
        }
    }
}
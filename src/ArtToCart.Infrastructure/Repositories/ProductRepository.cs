using ArtToCart.Application.Shared.Interfaces;
using ArtToCart.Domain.Products;
using ArtToCart.Domain.Products.ValueObjects;
using ArtToCart.Infrastructure.Shared;

using Microsoft.EntityFrameworkCore;

namespace ArtToCart.Infrastructure.Repositories;

public class ProductRepository : IRepository<CatalogItem>
{
    private readonly ArtToCartIdentityDbContext _context;

    public ProductRepository(ArtToCartIdentityDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<CatalogItem>> GetAllAsync()
    {
        var products = await _context.CatalogItems
            .Include(p => p.CatalogType)
            .Include(p => p.Images)
            .Include(p => p.Reviews)
            .ToListAsync();

        return products;
    }

    public async Task<IEnumerable<CatalogItem>> ListAsync(string[] ids)
    {
        var products = await _context.CatalogItems
            .Include(p => p.Images)
            .Include(p => p.Reviews)
            .ToListAsync();

        var selectedProducts = products.Where(p => ids.Contains(p.Id.Value.ToString()));

        return selectedProducts;
    }

    public async Task<CatalogItem> FirstOrDefaultAsync(string id)
    {
        var product = await _context.CatalogItems
            .Include(ci => ci.Images)
            .Include(ci => ci.Reviews)
            .FirstOrDefaultAsync(p => p.Id == CatalogItemId.CreateFrom(id));

        return product;

        // return (CatalogItem)!product; // assert that product is not null
    }

    public async Task AddAsync(CatalogItem entity)
    {
        await _context.CatalogItems.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(CatalogItem entity)
    {
        _context.CatalogItems.Update(entity);
        await _context.SaveChangesAsync();
    }
}
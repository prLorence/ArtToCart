using ArtToCart.Application.Shared.Interfaces;
using ArtToCart.Domain.Products;
using ArtToCart.Infrastructure.Shared;

using Microsoft.EntityFrameworkCore;

namespace ArtToCart.Infrastructure.Repositories;

public class ProductRepository<CatalogItem> : IRepository<CatalogItem>
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
            .ToListAsync();
        
        return (IEnumerable<CatalogItem>)products;
    }
}
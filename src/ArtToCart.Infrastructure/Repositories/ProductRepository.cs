using ArtToCart.Application.Shared.Interfaces;
using ArtToCart.Domain.Products;
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
            .ToListAsync();

        return (IEnumerable<CatalogItem>)products;
    }

    public async Task<CatalogItem> FirstOrDefaultAsync(string id)
    {
        var product = await _context.CatalogItems
            .FirstOrDefaultAsync(p => p.Id.Value == Guid.Parse(id));

        return product;

        // return (CatalogItem)!product; // assert that product is not null
    }

    public Task AddAsync(CatalogItem entity)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(CatalogItem entity)
    {
        throw new NotImplementedException();
    }
}
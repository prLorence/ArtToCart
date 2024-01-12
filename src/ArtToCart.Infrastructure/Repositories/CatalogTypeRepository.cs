using ArtToCart.Application.Shared.Interfaces;
using ArtToCart.Domain.Products;
using ArtToCart.Infrastructure.Shared;

using Microsoft.EntityFrameworkCore;

namespace ArtToCart.Infrastructure.Repositories;

public class CatalogTypeRepository : IRepository<CatalogType>
{
    private readonly ArtToCartDbContext _context;

    public CatalogTypeRepository(ArtToCartDbContext context)
    {
        _context = context;
    }
    public Task<IEnumerable<CatalogType>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<CatalogType>> ListAsync(string[] ids)
    {
        throw new NotImplementedException();
    }

    public async Task<CatalogType> FirstOrDefaultAsync(string type)
    {
        var catalogType = await _context.CatalogTypes
            .FirstOrDefaultAsync(ct => ct.Type == type);

        return catalogType;
    }

    public Task AddAsync(CatalogType entity)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(CatalogType entity)
    {
        throw new NotImplementedException();
    }
}
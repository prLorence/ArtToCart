using ArtToCart.Application.Shared.Interfaces;
using ArtToCart.Domain.Baskets;
using ArtToCart.Domain.Baskets.ValueObjects;
using ArtToCart.Infrastructure.Shared;

using Microsoft.EntityFrameworkCore;

namespace ArtToCart.Infrastructure.Repositories;

public class BasketRepository : IRepository<Basket>
{
    private readonly ArtToCartIdentityDbContext _context;

    public BasketRepository(ArtToCartIdentityDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Basket>> GetAllAsync()
    {
        var items = await _context.Basket
            .Include(b => b.Items)
            .ToListAsync();

        return items;
    }

    public async Task<Basket?> FirstOrDefaultAsync(string id)
    {
       var userItems = await _context.Basket
           .Where(b => b.BuyerId == id)
           .Include(b => b.Items)
           .FirstOrDefaultAsync() ?? await _context.Basket
               .Where(b => b.Id == BasketId.CreateFrom(id))
               .Include(b => b.Items)
               .FirstOrDefaultAsync();

       return userItems;
    }

    public async Task AddAsync(Basket entity)
    {
        await _context.Basket.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Basket entity)
    {
        _context.Update(entity);

        await _context.SaveChangesAsync();
    }
}
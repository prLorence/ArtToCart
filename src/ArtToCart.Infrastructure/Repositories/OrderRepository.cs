using ArtToCart.Application.Shared.Interfaces;
using ArtToCart.Domain.Orders;
using ArtToCart.Infrastructure.Shared;

using Microsoft.EntityFrameworkCore;

namespace ArtToCart.Infrastructure.Repositories;

public class OrderRepository : IRepository<Order>
{
    private readonly ArtToCartIdentityDbContext _context;

    public OrderRepository(ArtToCartIdentityDbContext context)
    {
        _context = context;
    }

    public async Task<Order> FirstOrDefaultAsync(string id)
    {
        var userOrders = await _context.Order
            .Where(o => o.Id.Value.ToString() == id)
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.ItemOrdered)
            .FirstOrDefaultAsync();

        return userOrders;
    }

    public async Task AddAsync(Order entity)
    {
        await _context.Order.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public Task UpdateAsync(Order entity)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Order>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Order>> ListAsync(string[] ids)
    {
        throw new NotImplementedException();
    }
}
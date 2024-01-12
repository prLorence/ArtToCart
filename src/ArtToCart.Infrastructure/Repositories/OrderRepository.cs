using ArtToCart.Application.Shared.Interfaces;
using ArtToCart.Domain.Orders;
using ArtToCart.Domain.Orders.ValueObjects;
using ArtToCart.Infrastructure.Shared;

using Microsoft.EntityFrameworkCore;

namespace ArtToCart.Infrastructure.Repositories;

public class OrderRepository : IRepository<Order>
{
    private readonly ArtToCartDbContext _context;

    public OrderRepository(ArtToCartDbContext context)
    {
        _context = context;
    }

    public async Task<Order> FirstOrDefaultAsync(string id)
    {
       var userOrders = await _context.Order
        .Where(o => o.Id == OrderId.CreateFrom(id))
        .Include(o => o.OrderItems)
        .ThenInclude(oi => oi.ItemOrdered)
        .FirstOrDefaultAsync()
           ?? await _context.Order
                .Where(o => o.BuyerId == id)
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

    public async Task<IEnumerable<Order>> ListAsync(string[] ids)
    {
        var products = await _context.Order
            .Include(o => o.OrderItems)
            .ToListAsync();

        var selectedProducts = products.Where(p => ids.Contains(p.BuyerId));

        return selectedProducts;
    }
}
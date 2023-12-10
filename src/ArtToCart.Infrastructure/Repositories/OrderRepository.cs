using ArtToCart.Application.Shared.Interfaces;
using ArtToCart.Domain.Orders;

namespace ArtToCart.Infrastructure.Repositories;

public class OrderRepository : IRepository<Order>
{

    public Task<IEnumerable<Order>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Order> FirstOrDefaultAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task AddAsync(Order entity)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Order entity)
    {
        throw new NotImplementedException();
    }
}
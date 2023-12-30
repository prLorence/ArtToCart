namespace ArtToCart.Application.Shared.Interfaces;

// TODO: this should be a query repository
public interface IRepository<T>
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<IEnumerable<T>> ListAsync(string[] ids);
    Task<T> FirstOrDefaultAsync(string id);
    // TODO: this should be in a command repository
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
}
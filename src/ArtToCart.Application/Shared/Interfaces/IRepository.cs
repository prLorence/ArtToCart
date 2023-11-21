namespace ArtToCart.Application.Shared.Interfaces;

public interface IRepository<T>
{
    Task<IEnumerable<T>> GetAllAsync();
}
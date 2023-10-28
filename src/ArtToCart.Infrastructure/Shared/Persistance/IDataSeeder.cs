namespace ArtToCart.Infrastructure.Shared.Persistance;

public interface IDataSeeder
{
    Task SeedAllAsync();
}
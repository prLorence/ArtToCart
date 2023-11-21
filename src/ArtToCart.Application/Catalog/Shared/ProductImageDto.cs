namespace ArtToCart.Application.Catalog.Shared;

public class ProductImageDto
{
    public Guid Id { get; init; }
    public string ImageUrl { get; init; } = default!;
    public bool IsMain { get; init; }
    public Guid CatalogItemId { get; init; }
}
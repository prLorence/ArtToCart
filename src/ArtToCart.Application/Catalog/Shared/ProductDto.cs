namespace ArtToCart.Application.Catalog.Shared;

public class ProductDto
{
    public Guid Id { get; init; }
    public string Name { get; init; } = default!;
    public decimal Price { get; init; }
    public string? Description { get; init; }
    public string Size { get; init; }
    public string SellerId { get; init; }
    public Guid CatalogTypeId { get; init; }
    public string CatalogType { get; init; } = default!;
    public double AverageRating { get; init; }
    public IEnumerable<ProductImageDto>? Images { get; init; }
    public IEnumerable<ItemReviewDto>? Reviews { get; init; }
}
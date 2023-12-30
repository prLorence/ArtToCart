using ArtToCart.Application.Catalog.Shared;

namespace ArtToCart.Application.Catalog.GetProduct;

public record GetProductResponse(List<ProductDto> CatalogItem);
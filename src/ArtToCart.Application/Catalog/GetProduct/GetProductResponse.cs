using ArtToCart.Application.Catalog.Shared;
using ArtToCart.Domain.Products;

namespace ArtToCart.Application.Catalog.GettingProducts;

public record GetProductResponse(ProductDto CatalogItem);
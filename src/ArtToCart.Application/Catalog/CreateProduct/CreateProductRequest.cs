using Microsoft.AspNetCore.Http;

namespace ArtToCart.Application.Catalog.CreateProduct;

public record CreateProductRequest(
    string Name,
    decimal Price,
    string Description,
    string Size,
    string ArtistId,
    string CatalogType,
    List<IFormFile> Images);
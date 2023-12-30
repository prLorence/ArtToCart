using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ArtToCart.Application.Catalog.CreateProduct;

public class CreateProductRequest
{

    [FromForm(Name = "name")]
    public string Name { get; init; }
    [FromForm(Name = "price")]
    public decimal Price { get; init; }
    [FromForm(Name = "description")]
    public string Description { get; init; }
    [FromForm(Name = "artistId")]
    public string ArtistId { get; init; }
    [FromForm(Name = "image")]
    public IFormFile Image { get; init; }
    [FromForm(Name = "catalogType")]
    public string CatalogType { get; init; }
}

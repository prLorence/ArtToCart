using ArtToCart.Web;

using MediatR;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ArtToCart.Application.Catalog.CreateProduct;

[Route("/products/create")]
public class CreateProductController : BaseController
{
    private readonly ISender _sender;

    public CreateProductController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromForm] CreateProductRequest request)
    {
        var images = new List<IFormFile> {request.Image};

        var command = new CreateProductCommand(
            request.Name,
            request.Price,
            request.Description,
            "X",
            request.ArtistId,
            request.CatalogType,
            images
        );

        var result = await _sender.Send(command);

        if (result.IsFailed)
        {
            return BadRequest(result.Errors.Select(e => e.Reasons));
        }

        return Ok(result.Value);
    }
}
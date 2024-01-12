using ArtToCart.Web;

using MediatR;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ArtToCart.Application.Catalog.CreateProduct;

[Route("/products/create")]
public class CreateProductController(ISender sender) : BaseController
{
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
            images
        );

        var result = await sender.Send(command);

        if (result.IsFailed)
        {
            return BadRequest(result.Errors.Select(e => e.Message));
        }

        return Ok(result.Value);
    }
}
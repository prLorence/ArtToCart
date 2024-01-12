using ArtToCart.Application.Catalog.GettingProducts;
using ArtToCart.Web;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace ArtToCart.Application.Catalog.GetProduct;


[Route("/products")]
public class GetProductController(ISender sender) : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetProducts([FromQuery] GetProductRequest request)
    {
        var query = new GetProductQuery(request.Id);

        var result = await sender.Send(query);

        if (result.IsFailed)
        {
            return BadRequest(result.Errors.Select(e => e.Message));
        }

        return Ok(result.Value);
    }
}
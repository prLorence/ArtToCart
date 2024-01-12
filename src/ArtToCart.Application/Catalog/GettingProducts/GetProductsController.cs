using ArtToCart.Web;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace ArtToCart.Application.Catalog.GettingProducts;


[Route("/products/all")]
public class GetProductsController(ISender sender) : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetProducts([FromQuery] GetProductsRequest request)
    {
        var query = new GetProductsQuery(request.OrderBy);

        var result = await sender.Send(query);

        return Ok(result.Value);
    }
}
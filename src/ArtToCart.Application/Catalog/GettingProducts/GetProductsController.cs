using ArtToCart.Web;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace ArtToCart.Application.Catalog.GettingProducts;


[Route("/products/[action]")]
public class GetProductsController : BaseController
{
    private readonly ISender _sender;

    public GetProductsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    public async Task<IActionResult> GetProducts([FromQuery] GetProductsRequest request)
    {
        var query = new GetProductsQuery();

        var result = await _sender.Send(query);

        return Ok(result.Value);
    }
}
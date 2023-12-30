using ArtToCart.Web;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace ArtToCart.Application.Catalog.GettingProducts;


[Route("/products/all")]
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
        var query = new GetProductsQuery(request.OrderBy);

        var result = await _sender.Send(query);

        return Ok(result.Value);
    }
}
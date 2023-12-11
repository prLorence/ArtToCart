using ArtToCart.Web;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace ArtToCart.Application.Catalog.GettingProducts;


[Route("/products")]
public class GetProductController : BaseController
{
    private readonly ISender _sender;

    public GetProductController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    public async Task<IActionResult> GetProducts([FromQuery] GetProductRequest request)
    {
        var query = new GetProductQuery(request.Id);

        var result = await _sender.Send(query);

        return Ok(result.Value);
    }
}
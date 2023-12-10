using ArtToCart.Web;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace ArtToCart.Application.Baskets.GetItemsFromBasket;

[Route("basket/items")]
public class GetItemsFromBasketController : BaseController
{
    private readonly ISender _sender;

    public GetItemsFromBasketController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    public async Task<IActionResult> GetItems([FromQuery] GetItemsFromBasketRequest request)
    {
        var query = new GetItemsFromBasketQuery(request.Username);

        var result = await _sender.Send(query);

        if (!result.IsSuccess)
        {
            return Ok(result.Errors.Select(e => e.Message));
        }

        return Ok(result.Value);
    }

}
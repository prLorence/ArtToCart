using ArtToCart.Web;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace ArtToCart.Application.Baskets.GetItemsFromBasket;

[Route("basket/items")]
public class GetItemsFromBasketController(ISender sender) : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetItems([FromQuery] GetItemsFromBasketRequest request)
    {
        var query = new GetItemsFromBasketQuery(request.Username);

        var result = await sender.Send(query);

        if (!result.IsSuccess)
        {
            return Ok(result.Errors.Select(e => e.Message));
        }

        return Ok(result.Value);
    }

}
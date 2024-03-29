using ArtToCart.Web;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace ArtToCart.Application.Baskets.UpdateBasketItems;

[Route("/basket/update")]
public class UpdateBasketItemsController(ISender sender) : BaseController
{
    [HttpPost]
    public async Task<IActionResult> UpdateItemQuantities(UpdateBasketItemsRequest request)
    {
        var command =
            new UpdateBasketItemsCommand(request.BasketId,
                request.Items.ToDictionary(
                    b => b.Id,
                    b => b.Quantity)
                );

        var result = await sender.Send(command);

        if (result.IsFailed)
        {
            return BadRequest(result.Errors.Select(e => e.Reasons));
        }

        return Ok(result.Value);
    }
}
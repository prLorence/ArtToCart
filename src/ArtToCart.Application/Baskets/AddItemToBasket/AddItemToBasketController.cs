using System.ComponentModel;

using ArtToCart.Web;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace ArtToCart.Application.Baskets.AddItemToBasket;

[Route("/basket/add")]
public class AddItemToBasketController(ISender sender) : BaseController
{
    [HttpPost]
    public async Task<IActionResult> AddItem(AddItemToBasketRequest request)
    {
        var command =
            new AddItemToBasketCommand(request.Username, request.Size, request.CatalogItemId, request.Quantity);

        var result = await sender.Send(command);

        if (!result.IsSuccess)
        {
            return Ok(result.Errors.Select(e => e.Message));
        }

        return Ok(result.Value);
    }
}
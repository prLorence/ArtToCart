using ArtToCart.Domain.Orders;
using ArtToCart.Web;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace ArtToCart.Application.Orders.CreateOrder;

[Route("/order/create")]
public class CreateOrderController : BaseController
{
    private readonly ISender _sender;

    public CreateOrderController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder(CreateOrderRequest request)
    {
        var shippingAddress = Address.Create(
            request.Address.Street,
            request.Address.City,
            request.Address.State,
            request.Address.Country,
            request.Address.ZipCode
        );

        var command = new CreateOrderCommand(request.BasketId, shippingAddress);

        var result = await _sender.Send(command);

        if (result.IsFailed)
        {
            return BadRequest(result.Errors.Select(e => e.Reasons));
        }

        return Ok(result.Value);
    }

}
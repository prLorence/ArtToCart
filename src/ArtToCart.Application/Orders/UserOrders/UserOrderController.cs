using ArtToCart.Web;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace ArtToCart.Application.Orders.UserOrders;

[Route("/user")]
public class UserOrderController : BaseController
{
    private readonly ISender _sender;

    public UserOrderController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet("order")]
    public async Task<IActionResult> GetOder([FromQuery] UserOrderRequest request)
    {
        var query = new UserOrderQuery(request.BuyerId);

        var result = await _sender.Send(query);

        if (result.IsFailed)
        {
            return BadRequest(result.Errors.Select(e => e.Reasons));
        }

        return Ok(result.Value);
    }
    
}
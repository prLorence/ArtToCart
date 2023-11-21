using ArtToCart.Web;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace ArtToCart.Application.Users.Features.GettingUsers;

[Route("/users/[action]")]
public class GetUsersController : BaseController
{
    private readonly ISender _sender;

    public GetUsersController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    public async Task<IActionResult> GetUsers([FromQuery] GetUsersRequest request)
    {
        var query = new GetUsersQuery();

        var result = await _sender.Send(query);

        return Ok(result.Value);
    }
}
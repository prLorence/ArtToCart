using ArtToCart.Web;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace ArtToCart.Application.Users.Features.LogInUser;

[Route("user/login")]
public class LoginUserController : BaseController
{
    private readonly ISender _sender;

    public LoginUserController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> LoginUser(LoginUserRequest request)
    {
        var query = new LogInUserQuery(request.Email, request.Password);

        var result = await _sender.Send(query);

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return Ok(result.Errors.Select(e => e.Message));
    }
}
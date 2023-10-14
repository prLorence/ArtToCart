using ArtToCart.Application.Identity.Shared;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace ArtToCart.Application.Identity.Users.Features.RegisteringUser;

[Route("/users/[action]")]
public class RegisterUserController: BaseController
{
    private readonly ISender _sender;

    public RegisterUserController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> Register([FromBody] RegisterUserRequest request)
    {
        var command = new RegisterUserCommand(
            request.FirstName,
            request.LastName,
            request.UserName,
            request.Email,
            request.Password,
            request.ConfirmPassword);

        var result = await _sender.Send(command);
        // return result;
        return Ok(result.Value.Result);
    }
}

using ArtToCart.Web;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace ArtToCart.Application.Users.Features.RegisteringUser;

[Route("/users/registration")]
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
            request.ConfirmPassword,
            request.Roles);

        var result = await _sender.Send(command);
        // return result;
        return Ok(result.Value);
    }
}

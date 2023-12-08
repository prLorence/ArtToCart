using ArtToCart.Web;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace ArtToCart.Application.Users.Features.ResetPassword;

[Route("account/reset-password")]
public class ResetPasswordController : BaseController
{
    private readonly ISender _sender;

    public ResetPasswordController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> ResetPassword(ResetPasswordRequest request)
    {
        var command = new ResetPasswordCommand(request.Email, request.NewPassword, request.Token);

        var result = await _sender.Send(command);

        return Ok(result.Value);
    }
}
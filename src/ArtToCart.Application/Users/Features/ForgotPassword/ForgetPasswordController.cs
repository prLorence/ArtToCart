using ArtToCart.Web;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace ArtToCart.Application.Users.Features.ForgotPassword;

[Route("user/ForgetPassword")]

public class ForgetPasswordController : BaseController
{
  private readonly ISender _sender;

  public ForgetPasswordController(ISender sender)
  {
    _sender = sender;
  }

    [HttpPost]
    [ValidateAntiForgeryToken]

    public async Task<IActionResult> ForgotPassword(ForgetPasswordRequest request)
    {
       var command  = new ForgetPassword(

        request.Email
       );
        var result = await _sender.Send(command);

        return Ok(result.Value);
    }
}
using ArtToCart.Web;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace ArtToCart.Application.Users.Features.AccountRecoveryTokens;

[Route("user/reset-token")]
public class AccountRecoveryTokenController : BaseController
{
  private readonly ISender _sender;

  public AccountRecoveryTokenController(ISender sender)
  {
    _sender = sender;
  }

    [HttpGet]
    public async Task<IActionResult> ForgotPassword([FromQuery] AccountRecoveryTokenRequest request)
    {
       var query  = new AccountRecoveryTokenQuery(request.Email);

       var result = await _sender.Send(query);

       if (result.IsFailed)
       {
           return Ok(result.Errors);
       }

        return Ok(result.Value);
    }
}
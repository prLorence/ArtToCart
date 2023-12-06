using ArtToCart.Web;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace ArtToCart.Application.Users.Features.AccountRecoveryTokens;

[Route("user/ForgetPassword")]
public class AccountRecoveryTokenController : BaseController
{
  private readonly ISender _sender;

  public AccountRecoveryTokenController(ISender sender)
  {
    _sender = sender;
  }

    [HttpPost]
    public async Task<IActionResult> ForgotPassword([FromBody] AccountRecoveryTokenRequest request)
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
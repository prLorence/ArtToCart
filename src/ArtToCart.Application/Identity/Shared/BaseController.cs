using Microsoft.AspNetCore.Mvc;

namespace ArtToCart.Application.Identity.Shared;

[ApiController]
public class BaseController : Controller
{
    protected ProblemDetails Problem(object errorDetails)
    {
        return new ProblemDetails();
    }
}
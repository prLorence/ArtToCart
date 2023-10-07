using Microsoft.AspNetCore.Mvc;

namespace ArtToCart.Web;

public class BaseController : Controller
{
    protected ProblemDetails Problem(object errorDetails)
    {
        return new ProblemDetails();
    }
}
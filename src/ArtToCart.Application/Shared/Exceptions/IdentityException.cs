using System.Net;

using ArtToCart.Application.Shared.Models;

namespace ArtToCart.Application.Shared.Exceptions;

public class IdentityException : CustomException
{
    public IdentityException(string message, List<string> errors = default, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        : base(message, statusCode, errors)
    {
    }

}
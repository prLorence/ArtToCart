namespace ArtToCart.Application.Identity.Users.Features.RegisteringUser;

public class RegisterUserException : Exception
{
    public RegisterUserException(string error) : base(error)
    {
    }
}
namespace ArtToCart.Application.Identity.Shared.Exceptions;

public class UserNotFoundException : Exception
{
    public UserNotFoundException(string emailOrUserName) : base($"User with email or username: '{emailOrUserName}' not found.")
    {
    }
}
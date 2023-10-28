using ArtToCart.Application.Shared.Models;

namespace ArtToCart.Application.Shared.Exceptions;

public class UserNotFoundException : CustomException
{
    public UserNotFoundException(string emailOrUserName) : base($"User with email or username: '{emailOrUserName}' not found.")
    {
    }
}
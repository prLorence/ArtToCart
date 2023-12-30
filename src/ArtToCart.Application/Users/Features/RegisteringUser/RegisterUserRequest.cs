using ArtToCart.Application.Identity;

namespace ArtToCart.Application.Users.Features.RegisteringUser;

public record RegisterUserRequest(
    string FirstName,
    string LastName,
    string UserName,
    string Email,
    string Password,
    string ConfirmPassword,
    List<string>? Roles);
using ArtToCart.Application.Identity.Users.Dtos;

namespace ArtToCart.Application.Identity.Users.Features.RegisteringUser;

public record RegisterUserResponse(string Result, IdentityUserDto? User);

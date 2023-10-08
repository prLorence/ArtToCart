using ArtToCart.Application.Users.Dtos;

namespace ArtToCart.Application.Users.Features.RegisteringUser;

public record RegisterUserResponse(string Result, IdentityUserDto? User);

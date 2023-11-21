using ArtToCart.Application.Shared.Models;
using ArtToCart.Application.Users.Dtos;

namespace ArtToCart.Application.Users.Features.GettingUsers;

public record GetUsersResponse(List<IdentityUserDto> Users);
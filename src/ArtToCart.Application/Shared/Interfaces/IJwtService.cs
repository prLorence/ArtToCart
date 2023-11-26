using System.Security.Claims;

namespace ArtToCart.Application.Shared.Interfaces;

public interface IJwtService
{
    string GenerateToken(
        string userName,
        string email,
        string userId,
        bool? isVerified = null,
        string? fullName = null,
        string? refreshToken = null,
        IReadOnlyList<Claim>? usersClaims = null,
        IReadOnlyList<string>? rolesClaims = null,
        IReadOnlyList<string>? permissionsClaims = null);

    ClaimsPrincipal? GetPrincipalFromToken(string token);
}
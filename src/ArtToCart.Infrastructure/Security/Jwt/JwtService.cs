using System.Security.Claims;

namespace ArtToCart.Infrastructure.Security.Jwt;

// should be used in the login handler
public class JwtService : IJwtService
{
    public string GenerateToken(string userName, string email, string userId, bool? isVerified = null, string? fullName = null,
        IReadOnlyList<Claim>? usersClaims = null, IReadOnlyList<string>? rolesClaims = null, IReadOnlyList<string>? permissionsClaims = null)
    {
        throw new NotImplementedException();
    }

    public ClaimsPrincipal? GetPrincipalFromToken(string token)
    {
        throw new NotImplementedException();
    }
}
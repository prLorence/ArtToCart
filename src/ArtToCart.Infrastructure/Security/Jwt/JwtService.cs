using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using ArtToCart.Application.Shared.Interfaces;
using ArtToCart.Infrastructure.Shared.Jwt;

using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace ArtToCart.Infrastructure.Security.Jwt;

// should be used in the login handler
public class JwtService : IJwtService
{
    private readonly JwtOptions _jwtOptions;

    public JwtService(IOptions<JwtOptions> jwtOptions)
    {
        _jwtOptions = jwtOptions.Value;
    }
    public string GenerateToken(string userName,
        string email,
        string userId,
        bool? isVerified = null,
        string? fullName = null,
        string? refreshToken = null,
        IReadOnlyList<Claim>? usersClaims = null,
        IReadOnlyList<string>? rolesClaims = null,
        IReadOnlyList<string>? permissionsClaims = null)
    {
        var now = DateTime.Now;

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.NameId, userId),
            new(JwtRegisteredClaimNames.Name, fullName ?? ""),
            new(JwtRegisteredClaimNames.Sub, userId),
            new(JwtRegisteredClaimNames.Sid, userId),
            new(JwtRegisteredClaimNames.UniqueName, userName),
            new(JwtRegisteredClaimNames.Email, email),
            new(JwtRegisteredClaimNames.GivenName, fullName ?? ""),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Iat,
                DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture)),

        };

        if (rolesClaims?.Any() is true)
        {
            foreach (var role in rolesClaims)
                claims.Add(new Claim(ClaimTypes.Role, role.ToLower(CultureInfo.InvariantCulture)));
        }

        if (!string.IsNullOrWhiteSpace(_jwtOptions.Audience))
            claims.Add(new Claim(JwtRegisteredClaimNames.Aud, _jwtOptions.Audience));

        if (permissionsClaims?.Any() is true)
        {
            foreach (var permissionsClaim in permissionsClaims)
            {
                claims.Add(new Claim(
                    CustomClaimTypes.Permission,
                    permissionsClaim.ToLower(CultureInfo.InvariantCulture)));
            }
        }

        SymmetricSecurityKey signingKey = new(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey));
        SigningCredentials signingCredentials = new(signingKey, SecurityAlgorithms.HmacSha256);

        var jwt = new JwtSecurityToken(
            _jwtOptions.Issuer,
            _jwtOptions.Audience,
            notBefore: now,
            claims: claims,
            expires: now.AddSeconds(_jwtOptions.TokenLifeTimeSecond == 0 ? 36000 : _jwtOptions.TokenLifeTimeSecond),
            signingCredentials: signingCredentials);

        var token = new JwtSecurityTokenHandler().WriteToken(jwt);

        return token;
    }

    public ClaimsPrincipal? GetPrincipalFromToken(string token)
    {
        TokenValidationParameters tokenValidationParameters = new()
        {
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey)),
            ValidateLifetime = false,
            ClockSkew = TimeSpan.Zero,
        };

        JwtSecurityTokenHandler tokenHandler = new();

        ClaimsPrincipal principal = tokenHandler.ValidateToken(
            token,
            tokenValidationParameters,
            out SecurityToken securityToken);

        JwtSecurityToken? jwtSecurityToken = securityToken as JwtSecurityToken;

        if (jwtSecurityToken == null)
        {
            throw new SecurityTokenException("Invalid access token.");
        }

        return principal;
    }
}
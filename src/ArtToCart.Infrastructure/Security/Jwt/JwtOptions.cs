namespace ArtToCart.Infrastructure.Security.Jwt;

public class JwtOptions
{
    public const string SectionName = "JwtOptions";

    public string? Algorithm { get; set; }
    public string? Issuer { get; set; }
    public string SecretKey { get; set; } = null!;
    public string? Audience { get; set; }
    public double TokenLifeTimeSecond { get; set; }
}
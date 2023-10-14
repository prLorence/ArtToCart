using Microsoft.AspNetCore.Identity;

namespace ArtToCart.Application.Shared.Models;

public class ApplicationUser : IdentityUser<Guid>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime? CreatedAt { get; set; }

    public UserState UserState { get; set; }
    public ICollection<RefreshToken> RefreshTokens { get; set; }
    public DateTime? LastLoggedInAt { get; set; }
}
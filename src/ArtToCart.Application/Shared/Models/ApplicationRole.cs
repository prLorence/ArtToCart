using System.Globalization;

using ArtToCart.Application.Identity;

using Microsoft.AspNetCore.Identity;

namespace ArtToCart.Application.Shared.Models;

public class ApplicationRole : IdentityRole<Guid>
{
    private static readonly Guid ADMIN_ID = Guid.Parse("2c5e174e-3b0e-446f-86af-483d56fd7210");
    private static readonly Guid USER_ID = Guid.Parse("a18be9c0-aa65-4af8-bd17-00bd9344e575");

    public static ApplicationRole User => new()
    {
        Id = ADMIN_ID, Name = Constants.Role.User, NormalizedName = nameof(User).ToUpper(CultureInfo.InvariantCulture),
    };

    public static ApplicationRole Admin => new()
    {
        Id = USER_ID,
        Name = Constants.Role.Admin,
        NormalizedName = nameof(Admin).ToUpper(CultureInfo.InvariantCulture)
    };
}
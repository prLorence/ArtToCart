using ArtToCart.Infrastructure.Identity.Models;
using ArtToCart.Infrastructure.Shared;
using ArtToCart.Modules.Identity.Shared.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ArtToCart.Infrastructure;

public static class DepedencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {

        services.AddDbContext<ArtToCartDbContext>(
                opts => opts.UseNpgsql(config.GetConnectionString("ArtToCartPsqlConnection"))
            )
            .AddIdentityCore<ApplicationUser>()
            .AddRoles<ApplicationRole>()
            .AddEntityFrameworkStores<ArtToCartDbContext>();

        return services;
    }

}
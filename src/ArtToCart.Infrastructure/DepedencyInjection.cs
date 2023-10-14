using ArtToCart.Application.Shared.Models;
using ArtToCart.Infrastructure.Security.Jwt;
using ArtToCart.Infrastructure.Shared;

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

    // public static IServiceCollection AddCustomJwtAuthentication(this IServiceCollection services, )
    public static IServiceCollection AddJwt(this IServiceCollection services, IConfiguration config)
    {
        // configure from appsettings.json
        services.AddTransient<IJwtService, JwtService>();
        return services;
    }

}
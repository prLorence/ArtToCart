using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Reflection;
using System.Text;

using ArtToCart.Application.Shared.Exceptions;
using ArtToCart.Application.Shared.Interfaces;
using ArtToCart.Application.Shared.Models;
using ArtToCart.Domain.Baskets;
using ArtToCart.Domain.Orders;
using ArtToCart.Domain.Products;
using ArtToCart.Infrastructure.Data;
using ArtToCart.Infrastructure.Repositories;
using ArtToCart.Infrastructure.Security.Jwt;
using ArtToCart.Infrastructure.Shared;
using ArtToCart.Infrastructure.Shared.Persistance;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ArtToCart.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        AddAuthentication(services, config);

        services.AddDbContext<ArtToCartDbContext>(options =>
                    options.UseNpgsql(config.GetConnectionString("ArtToCartPsqlConnection"))
                )
                .AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<ArtToCartDbContext>()
                .AddDefaultTokenProviders();

        services.Configure<IdentityOptions>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;
            });

        services.AddScoped<IDataSeeder, IdentityDataSeeder>();
        services.AddScoped<IDataSeeder, CatalogDataSeeder>();

        services.AddTransient(typeof(IRepository<Basket>), typeof(BasketRepository));
        services.AddTransient(typeof(IRepository<CatalogItem>), typeof(ProductRepository));
        services.AddTransient(typeof(IRepository<Order>), typeof(OrderRepository));
        services.AddTransient(typeof(IRepository<CatalogType>), typeof(CatalogTypeRepository));

        services.AddSingleton(typeof(IJwtService), typeof(JwtService));

        return services;
    }

    public static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtSettings = new JwtOptions();

        configuration.Bind(JwtOptions.SectionName, jwtSettings);

        services.AddSingleton(Options.Create(jwtSettings));

        services.AddSingleton<IJwtService, JwtService>();

        services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
          .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
          {
              ValidateIssuer = true,
              ValidateAudience = true,
              ValidateLifetime = true,
              ValidateIssuerSigningKey = true,
              IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey)),
              ValidIssuer = jwtSettings.Issuer,
              ValidAudience = jwtSettings.Audience,
          });

        return services;
    }
}
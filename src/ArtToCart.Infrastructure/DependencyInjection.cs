using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Text;

using ArtToCart.Application.Shared.Exceptions;
using ArtToCart.Application.Shared.Models;
using ArtToCart.Infrastructure.Data;
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

        services.AddDbContext<ArtToCartIdentityDbContext>(options =>
                    options.UseNpgsql(config.GetConnectionString("ArtToCartPsqlConnection"))
                )
                .AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<ArtToCartIdentityDbContext>()
                .AddDefaultTokenProviders();

        services.AddScoped<IDataSeeder, IdentityDataSeeder>();

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
using System.Reflection;

using ArtToCart.Application.Catalog.Shared.MapConfigs;
using ArtToCart.Application.Shared;
using ArtToCart.Application.Users.Features.RegisteringUser;

using FluentValidation;

using MediatR;

using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ArtToCart.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        services.AddMapster();

        services.AddAntiforgery();

        services.AddMvc();

        return services;
    }

    public static IServiceCollection AddAzure(this IServiceCollection services, IConfiguration config)
    {
        var connectionString = config.GetConnectionString("AzureBlobStorage");

        services.AddAzureClients(azureBuilder =>
        {
            azureBuilder.AddBlobServiceClient(connectionString);
        });

        return services;
    }
}
using System.Reflection;

using ArtToCart.Application.Identity.Users.Features.RegisteringUser;

using MediatR;

using Microsoft.Extensions.DependencyInjection;

namespace ArtToCart.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(RegisterUserValidation));

        return services;
    }
}
﻿using System.Reflection;

using ArtToCart.Application.Catalog.Shared.MapConfigs;
using ArtToCart.Application.Shared;
using ArtToCart.Application.Users.Features.RegisteringUser;

using FluentValidation;

using MediatR;

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
}
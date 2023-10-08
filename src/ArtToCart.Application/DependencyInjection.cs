﻿using System.Reflection;

using ArtToCart.Infrastructure;

using MediatR;

using Microsoft.Extensions.DependencyInjection;

namespace ArtToCart.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

        return services;
    }
}
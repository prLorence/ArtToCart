using ArtToCart.Application;
using ArtToCart.Application.Shared.Models;
using ArtToCart.Infrastructure;
using ArtToCart.Infrastructure.Data;
using ArtToCart.Infrastructure.Shared;
using ArtToCart.Infrastructure.Shared.Persistance;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var scopedProvider = scope.ServiceProvider;
    try
    {
        // var catalogContext = scopedProvider.GetRequiredService<CatalogContext>();
        // await CatalogContextSeed.SeedAsync(catalogContext, app.Logger);

        var seeders = scopedProvider.GetServices<IDataSeeder>();

        foreach (var seeder in seeders)
        {
            app.Logger.LogInformation("Seeding '{Seed}' started...", seeder.GetType().Name);
            await seeder.SeedAllAsync();
            app.Logger.LogInformation("Seeding '{Seed}' ended...", seeder.GetType().Name);
        }

        // var userManager = scopedProvider.GetRequiredService<>();
        //
        // var identityContext = scopedProvider.GetRequiredService<ArtToCartIdentityDbContext>();
        //
        // identityContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        //
        // await IdentityDataSeeder.SeedRoles(identityContext, userManager, roleManager);
    }
    catch (Exception ex)
    {
        app.Logger.LogError(ex, "An error occurred seeding the DB.");
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

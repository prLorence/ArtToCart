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
builder.Services.AddAzure(builder.Configuration);
builder.Services.AddInfrastructure(builder.Environment, builder.Configuration);

var app = builder.Build();

// app.Logger.LogInformation("Running the application in '{environmentName}' environment", app.Environment.EnvironmentName);

using (var scope = app.Services.CreateScope())
{
    var scopedProvider = scope.ServiceProvider;
    try
    {
        // var catalogContext = scopedProvider.GetRequiredService<ArtToCartDbContext>();
        // await CatalogContextSeed.SeedAsync(catalogContext, app.Logger);

        var seeders = scopedProvider.GetServices<IDataSeeder>();

        foreach (var seeder in seeders)
        {
            app.Logger.LogInformation("Seeding '{Seed}' started...", seeder.GetType().Name);
            await seeder.SeedAllAsync();
            app.Logger.LogInformation("Seeding '{Seed}' ended...", seeder.GetType().Name);
        }
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

app.UseCors(options =>  options
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true) // allow any origin
    //.WithOrigins("https://localhost:44351")); // Allow only this origin can also have multiple origins separated with comma
    .AllowCredentials()); // allow credentials;

app.UseAuthorization();

app.MapControllers();

app.Run();

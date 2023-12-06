using ArtToCart.Application.Shared.Models;
using ArtToCart.Infrastructure.Shared;
using ArtToCart.Infrastructure.Shared.Persistance;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ArtToCart.Infrastructure.Data;

public class IdentityDataSeeder : IDataSeeder
{
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public IdentityDataSeeder(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task SeedAllAsync()
    {
        await SeedRoles();
        await SeedUsers();
    }

    private async Task SeedRoles()
    {
        var adminRole = await _roleManager.FindByNameAsync(ApplicationRole.Admin.Name);
        var userRole = await _roleManager.FindByNameAsync(ApplicationRole.User.Name);
        var artistRole = await _roleManager.FindByNameAsync(ApplicationRole.Artist.Name);

        if (artistRole == null)
        {
            await _roleManager.CreateAsync(ApplicationRole.Artist);
        }

        if (adminRole == null)
        {
            await _roleManager.CreateAsync(ApplicationRole.Admin);
        }

        if (userRole == null)
        {
            await _roleManager.CreateAsync(ApplicationRole.User);
        }
    }

    private async Task SeedUsers()
    {
        string adminGuid = "3229ad94-821d-43b4-9c38-9112b06a38ae";
        string userGuid = "4b7db584-4b58-4d23-9d3a-c8d95b4f140a";
        string artistGuid = "08cd02b8-2682-4569-92a7-987e1ec28ec1";

        if (await _userManager.FindByEmailAsync("admin@test.com") == null)
        {
            var user = new ApplicationUser
            {
                Id = Guid.Parse(adminGuid),
                UserName = "test123",
                FirstName = "Test",
                LastName = "Tester",
                Email = "example@test.com",
            };

            var result = await _userManager.CreateAsync(user, "Test1234!");

            if (result.Succeeded) await _userManager.AddToRoleAsync(user, ApplicationRole.Admin.Name);
        }

        if (await _userManager.FindByEmailAsync("user@test.com") == null)
        {
            var user = new ApplicationUser
            {
                Id = Guid.Parse(userGuid),
                UserName = "user1",
                FirstName = "first name user",
                LastName = "last name user",
                Email = "user@test.com"
            };

            var result = await _userManager.CreateAsync(user, "Test123!");

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, ApplicationRole.User.Name);
            }
        }

        if (await _userManager.FindByEmailAsync("artist@test.com") == null)
        {
            var user = new ApplicationUser
            {
                Id = Guid.Parse(artistGuid),
                UserName = "artist1",
                FirstName = "first name artist",
                LastName = "last name artist",
                Email = "artist@test.com"
            };

            var result = await _userManager.CreateAsync(user, "Test123!");

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, ApplicationRole.Artist.Name);
            }
        }
    }
}


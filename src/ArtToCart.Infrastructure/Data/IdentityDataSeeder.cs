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
        // await SeedUsers();
    }

    private async Task SeedRoles()
    {
        var adminRole = await _roleManager.FindByNameAsync(ApplicationRole.Admin.Name);
        var userRole = await _roleManager.FindByNameAsync(ApplicationRole.User.Name);

        if (adminRole == null)
        {
            await _roleManager.CreateAsync(ApplicationRole.Admin);
        }

        if (userRole == null)
        {
            await _roleManager.CreateAsync(ApplicationRole.User);
        }
    }

    // private async Task SeedRoles()
    // {
    //     if (!await _roleManager.RoleExistsAsync(ApplicationRole.Admin.Name))
    //         await _roleManager.CreateAsync(ApplicationRole.Admin);
    //
    //     if (!await _roleManager.RoleExistsAsync(ApplicationRole.User.Name))
    //         await _roleManager.CreateAsync(ApplicationRole.User);
    // }

    // private async Task SeedUsers()
    // {
    //     if (await _userManager.FindByEmailAsync("mehdi@test.com") == null)
    //     {
    //         var user = new ApplicationUser
    //         {
    //             UserName = "mehdi",
    //             FirstName = "Mehdi",
    //             LastName = "test",
    //             Email = "mehdi@test.com",
    //         };
    //
    //         var result = await _userManager.CreateAsync(user, "123456");
    //
    //         if (result.Succeeded) await _userManager.AddToRoleAsync(user, ApplicationRole.Admin.Name);
    //     }
    //
    //     if (await _userManager.FindByEmailAsync("mehdi2@test.com") == null)
    //     {
    //         var user = new ApplicationUser
    //         {
    //             UserName = "mehdi2", FirstName = "Mehdi", LastName = "Test", Email = "mehdi2@test.com"
    //         };
    //
    //         var result = await _userManager.CreateAsync(user, "123456");
    //
    //         if (result.Succeeded) await _userManager.AddToRoleAsync(user, ApplicationRole.User.Name);
    //     }
    // }
    //
}


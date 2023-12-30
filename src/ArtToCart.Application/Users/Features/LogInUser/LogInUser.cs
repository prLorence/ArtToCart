using System.Runtime.InteropServices;

using ArtToCart.Application.Shared.Interfaces;
using ArtToCart.Application.Shared.Models;

using FluentResults;

using FluentValidation;

using MediatR;

using Microsoft.AspNetCore.Identity;

namespace ArtToCart.Application.Users.Features.LogInUser;

public record LogInUserQuery(
    string Email,
    string Password) : IRequest<Result<LogInUserResponse>>;

 internal class LogInUserValidation : AbstractValidator<LogInUserQuery>
{
    public LogInUserValidation()
    {
        CascadeMode = CascadeMode.Stop;

        RuleFor(q => q.Email)
            .NotEmpty()
            .WithMessage("Email is Required");

        RuleFor(q => q.Password)
            .NotEmpty()
            .WithMessage("Password is Required");
    }
}

 public class LogInUserQueryHandler : IRequestHandler<LogInUserQuery, Result<LogInUserResponse>>
 {
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly IJwtService _jwtService;

    public LogInUserQueryHandler(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IJwtService jwtService, RoleManager<ApplicationRole> roleManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _jwtService = jwtService;
        _roleManager = roleManager;
    }

    public async Task<Result<LogInUserResponse>> Handle(LogInUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user is null)
        {
            return Result.Fail($"User with email {request.Email} doesn't exist");
        }

        var signInResult = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

        if (!signInResult.Succeeded)
        {
            return Result.Fail("Invalid password");
        }

        var roles = await _userManager.GetRolesAsync(user);
        var token = _jwtService.GenerateToken(user.UserName!, user.Email!, user.Id.ToString(), rolesClaims: roles.AsReadOnly());

        return new LogInUserResponse
        {
            Id = user.Id.ToString(),
            Username = user.UserName!,
            Role = roles[0],
            Token = token
        };
    }
}
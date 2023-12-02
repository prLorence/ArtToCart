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
            .WithMessage("Username is Required");

        RuleFor(q => q.Password)
            .NotEmpty()
            .WithMessage("Password is Required");
    }
}

 public class LogInUserQueryHandler : IRequestHandler<LogInUserQuery, Result<LogInUserResponse>>
 {
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IJwtService _jwtService;

    public LogInUserQueryHandler(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IJwtService jwtService)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _jwtService = jwtService;
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
            return new LogInUserResponse { FailedObjectResult = signInResult };
        }

        var roles = await _userManager.GetRolesAsync(user);
        var token = _jwtService.GenerateToken(user.UserName!, user.Email!, user.Id.ToString(), rolesClaims: roles.AsReadOnly());

        return new LogInUserResponse { Token = token, Message = "Log in success" };
    }
}
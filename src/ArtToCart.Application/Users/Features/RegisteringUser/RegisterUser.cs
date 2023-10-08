using ArtToCart.Modules.Identity.Shared.Models;
using ArtToCart.Web;

using FluentResults;

using MediatR;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ArtToCart.Application.Users.Features.RegisteringUser;

public record RegisterUserCommand(
    string FirstName,
    string LastName,
    string UserName,
    string Email,
    string Password,
    string ConfirmPassword) : IRequest<Result<RegisterUserResponse>>
{
    public DateTime CreatedAt { get; init; } = DateTime.Now;
    public Guid Id { get; init; } = Guid.NewGuid();
}

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Result<RegisterUserResponse>>
{
    private readonly UserManager<ApplicationUser> _userManager;

    public RegisterUserCommandHandler(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }
    public async Task<Result<RegisterUserResponse>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var newUser = new ApplicationUser
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            UserName = request.UserName,
            Email = request.Email
        };

        var createUserResult = await _userManager.CreateAsync(newUser, request.Password);

        if (!createUserResult.Succeeded)
            return Result.Fail(string.Join(",", createUserResult.Errors.Select(e => e.Description)));

        return new RegisterUserResponse("registered user!", null);
    }
}
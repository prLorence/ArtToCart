using ArtToCart.Application.Identity;
using ArtToCart.Application.Shared.Models;
using ArtToCart.Application.Users.Dtos;
using ArtToCart.Web;

using FluentResults;

using FluentValidation;

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
    string ConfirmPassword,
    List<string>? Roles = null) : IRequest<Result<RegisterUserResponse>>
{
    public DateTime CreatedAt { get; init; } = DateTime.Now.ToUniversalTime();
    public Guid Id { get; init; } = Guid.NewGuid();
}

// add fluent validation for property validations
// add this to the mediatr pipeline
internal class RegisterUserValidation : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserValidation()
    {
        CascadeMode = CascadeMode.Stop;

        RuleFor(u => u.FirstName)
            .NotEmpty()
            .WithMessage("First Name is Required");

        RuleFor(u => u.LastName)
            .NotEmpty()
            .WithMessage("Last Name is Required");

        RuleFor(u => u.Email)
            .NotEmpty()
            .WithMessage("Email is Required");

        RuleFor(u => u.UserName)
            .NotEmpty()
            .WithMessage("Username is Required");

        RuleFor(u => u.Password)
            .NotEmpty()
            .WithMessage("Password is Required");

        RuleFor(u => u.ConfirmPassword)
            .Equal(u => u.Password)
            .WithMessage("The password and confirmation password do not match")
            .NotEmpty();

        RuleFor(v => v.Roles).Custom((roles, c) =>
        {
            if (roles != null &&
                !roles.All(x => x.Contains(Constants.Role.Admin, StringComparison.Ordinal) ||
                                x.Contains(Constants.Role.User, StringComparison.Ordinal)))
                // add the validation for the artist role
            {
                c.AddFailure("Invalid roles.");
            }
        });
    }
}

internal class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Result<RegisterUserResponse>>
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
            Id = request.Id,
            FirstName = request.FirstName,
            LastName = request.LastName,
            UserName = request.UserName,
            Email = request.Email,
            UserState = UserState.Active,
            CreatedAt = request.CreatedAt,
            SecurityStamp = Guid.NewGuid().ToString("D"),
        };

        var createUserResult = await _userManager.CreateAsync(newUser, request.Password);

        if (!createUserResult.Succeeded)
            return Result.Fail(string.Join(",", createUserResult.Errors.Select(e => e.Description)));

        var roleResult = await _userManager.AddToRolesAsync(newUser, request.Roles ?? new List<string>{Constants.Role.User});

        if (!roleResult.Succeeded)
            return Result.Fail(string.Join(",", createUserResult.Errors.Select(e => e.Description)));

        return new RegisterUserResponse(new IdentityUserDto
        {
            Id = newUser.Id,
            Email = newUser.Email,
            UserName = newUser.UserName,
            FirstName = newUser.FirstName,
            LastName = newUser.LastName,
            Roles = request.Roles ?? new List<string> {Constants.Role.User},
            RefreshTokens = newUser?.RefreshTokens?.Select(x => x.Token),
            CreatedAt = request.CreatedAt,
            UserState = UserState.Active
        });
    }
}
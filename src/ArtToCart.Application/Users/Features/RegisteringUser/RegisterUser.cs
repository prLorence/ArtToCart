using ArtToCart.Modules.Identity.Shared.Models;
using ArtToCart.Web;

using FluentResults;

using MediatR;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ArtToCart.Application.Users.Features.RegisteringUser;

public class RegisterUserController : BaseController
{
    private readonly ISender _sender;

    public RegisterUserController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<ActionResult> Register([FromBody] RegisterUserCommand command)
    {
        var result = await _sender.Send(command);
        // return result;
        return Ok(result.Value);
    }
}

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


        return new RegisterUserResponse(null);
    }
}
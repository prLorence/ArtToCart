using ArtToCart.Application.Shared.Models;

using FluentResults;

using FluentValidation;

using MediatR;

using Microsoft.AspNetCore.Connections.Features;
using Microsoft.AspNetCore.Identity;

namespace ArtToCart.Application.Users.Features.ResetPassword;

public record ResetPasswordCommand
    (string Email, string NewPassword, string Token) : IRequest<Result<ResetPasswordResponse>>;

public class ResetPasswordCommandValidator : AbstractValidator<ResetPasswordCommand>
{
    public ResetPasswordCommandValidator()
    {
        CascadeMode = CascadeMode.Stop;

        RuleFor(rp => rp.Email)
            .NotEmpty()
            .WithMessage("Email should not be empty!");

        RuleFor(rp => rp.NewPassword)
            .NotEmpty()
            .WithMessage("New password should not be empty!");

        RuleFor(rp => rp.Token)
            .NotEmpty()
            .WithMessage("Token should not be empty!");
    }
}

public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, Result<ResetPasswordResponse>>
{
    private readonly UserManager<ApplicationUser> _userManager;

    public ResetPasswordCommandHandler(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<Result<ResetPasswordResponse>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user == null)
        {
            return Result.Fail($"User with email {request.Email} doesn't exist");
        }

        var resetPasswordResult = await _userManager.ResetPasswordAsync(user, request.Token, request.NewPassword);

        if (!resetPasswordResult.Succeeded)
        {
            return Result.Fail(resetPasswordResult.Errors.Select(e => e.Description));
        }

        return new ResetPasswordResponse("Reset password successful");
    }
}


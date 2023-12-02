using ArtToCart.Application.Shared.Interfaces;
using ArtToCart.Application.Shared.Models;

using FluentResults;

using FluentValidation;

using MediatR;

using Microsoft.AspNetCore.Identity;

using Microsoft.AspNetCore.Mvc;


namespace ArtToCart.Application.Users.Features.ForgotPassword;

public record ForgetPassword

    (string Email) : IRequest<Result<ForgetPasswordResponse>>;

internal class ForgetPassValidation : AbstractValidator<ForgetPassword>
{
    public ForgetPassValidation()
    {
        CascadeMode = CascadeMode.Stop;

        RuleFor(f => f.Email)
            .NotEmpty()
            .WithMessage("Email is Required!");
    }   
}

public class ForgetPasswordCommandHandler : IRequestHandler<ForgetPassword, Result<ForgetPasswordResponse>>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IJwtService _jwtService;

    public ForgetPasswordCommandHandler(UserManager<ApplicationUser> userManager, IJwtService jwtService)
    {
        _userManager = userManager;
        _jwtService = jwtService;
    }

    public async Task<Result<ForgetPasswordResponse>> Handle(ForgetPassword request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if(user is null)
        {
            return Result.Fail($"Email {request.Email} doesn't exist");
        }

        var token = _jwtService.GenerateToken(user.UserName!, user.Email!,user.Id.ToString());

        return new ForgetPasswordResponse
        {
            Email = user.Email,
            Token = token
        };
    }
}
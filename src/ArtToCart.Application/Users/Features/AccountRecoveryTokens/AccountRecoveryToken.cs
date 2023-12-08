using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;

using ArtToCart.Application.Shared.Interfaces;
using ArtToCart.Application.Shared.Models;

using FluentResults;

using FluentValidation;

using MediatR;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;

namespace ArtToCart.Application.Users.Features.AccountRecoveryTokens;

public record AccountRecoveryTokenQuery
    (string Email) : IRequest<Result<AccountRecoveryTokenResponse>>;

internal class ForgetPassValidation : AbstractValidator<AccountRecoveryTokenQuery>
{
    public ForgetPassValidation()
    {
        CascadeMode = CascadeMode.Stop;

        RuleFor(f => f.Email)
            .NotEmpty()
            .WithMessage("Email is Required!");
    }   
}

public class ForgetPasswordCommandHandler : IRequestHandler<AccountRecoveryTokenQuery, Result<AccountRecoveryTokenResponse>>
{
    private readonly UserManager<ApplicationUser> _userManager;

    public ForgetPasswordCommandHandler(UserManager<ApplicationUser> userManager, IJwtService jwtService)
    {
        _userManager = userManager;
    }

    public async Task<Result<AccountRecoveryTokenResponse>> Handle(AccountRecoveryTokenQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user == null)
        {
            return Result.Fail($"User with {request.Email} doesn't exist");
        }

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);

        return new AccountRecoveryTokenResponse
        {
            Email = user!.Email,
            Token = token
        };
    }
}
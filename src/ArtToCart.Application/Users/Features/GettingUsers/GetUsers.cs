using System.Collections;

using ArtToCart.Application.Shared.Models;
using ArtToCart.Application.Users.Dtos;

using FluentResults;

using Mapster;

using MapsterMapper;

using MediatR;

using Microsoft.AspNetCore.Identity;

namespace ArtToCart.Application.Users.Features.GettingUsers;

public record GetUsersQuery() : IRequest<Result<GetUsersResponse>>;

public class GetUser : IRequestHandler<GetUsersQuery, Result<GetUsersResponse>>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IMapper _mapper;

    public GetUser(UserManager<ApplicationUser> userManager, IMapper mapper)
    {
        _userManager = userManager;
        _mapper = mapper;
    }
    public async Task<Result<GetUsersResponse>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        var users = _userManager.Users
            .OrderByDescending(u => u.CreatedAt)
            .ToList();

        var result = _mapper.Map<List<IdentityUserDto>>(users);
        return new GetUsersResponse(result);
    }
}

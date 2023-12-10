using System.Net.Mime;

using ArtToCart.Application.Baskets.Shared;
using ArtToCart.Application.Catalog.GettingProducts;
using ArtToCart.Application.Shared.Interfaces;
using ArtToCart.Application.Shared.Models;
using ArtToCart.Domain.Baskets;

using FluentResults;

using FluentValidation;

using MapsterMapper;

using MediatR;

using Microsoft.AspNetCore.Identity;

namespace ArtToCart.Application.Baskets.GetItemsFromBasket;

public record GetItemsFromBasketQuery(string Username) : IRequest<Result<GetItemsFromBasketResponse>>;

public class GetItemsFromBasketQueryValidator : AbstractValidator<GetItemsFromBasketQuery>
{
    public GetItemsFromBasketQueryValidator()
    {
        CascadeMode = CascadeMode.Stop;

        RuleFor(q => q.Username)
            .NotEmpty()
            .WithMessage("Username field is required");
    }
}

public class GetItemsFromBasketQueryHandler : IRequestHandler<GetItemsFromBasketQuery, Result<GetItemsFromBasketResponse>>
{
    private readonly IRepository<Basket> _repository;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IMapper _mapper;

    public GetItemsFromBasketQueryHandler(IRepository<Basket> repository, UserManager<ApplicationUser> userManager, IMapper mapper)
    {
        _repository = repository;
        _userManager = userManager;
        _mapper = mapper;
    }

    public async Task<Result<GetItemsFromBasketResponse>> Handle(GetItemsFromBasketQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(request.Username);

        if (user is null)
        {
            return Result.Fail($"User with user name {request.Username} doesn't exist");
        }

        var basket = await _repository.FirstOrDefaultAsync(user.Id.ToString());

        // if (basket == null)
        // {
        //     basket = Basket.Create(user.Id.ToString());
        //     await _repository.AddAsync(basket);
        // }

        var result = _mapper.Map<BasketDto>(basket);

        return new GetItemsFromBasketResponse(result);

    }
}
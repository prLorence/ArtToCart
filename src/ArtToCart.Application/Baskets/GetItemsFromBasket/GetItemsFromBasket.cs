using System.Net.Mime;

using ArtToCart.Application.Baskets.Shared;
using ArtToCart.Application.Catalog.GettingProducts;
using ArtToCart.Application.Shared.Interfaces;
using ArtToCart.Application.Shared.Models;
using ArtToCart.Domain.Baskets;
using ArtToCart.Domain.Products;

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

public class GetItemsFromBasketQueryHandler(
    IRepository<Basket> repository,
    UserManager<ApplicationUser> userManager,
    IMapper mapper,
    IRepository<CatalogItem> productRepository)
    : IRequestHandler<GetItemsFromBasketQuery, Result<GetItemsFromBasketResponse>>
{
    private readonly IRepository<CatalogItem> _productRepository = productRepository;

    public async Task<Result<GetItemsFromBasketResponse>> Handle(GetItemsFromBasketQuery request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByNameAsync(request.Username);

        if (user is null)
        {
            return Result.Fail($"User with user name {request.Username} doesn't exist");
        }

        var basket = await repository.FirstOrDefaultAsync(user.Id.ToString());

        // if (basket == null)
        // {
        //     basket = Basket.Create(user.Id.ToString());
        //     await _repository.AddAsync(basket);
        // }

        var result = mapper.Map<BasketDto>(basket);

        return new GetItemsFromBasketResponse(result);

    }
}
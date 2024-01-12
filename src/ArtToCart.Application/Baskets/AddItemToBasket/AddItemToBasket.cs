using ArtToCart.Application.Baskets.Shared;
using ArtToCart.Application.Shared.Interfaces;
using ArtToCart.Application.Shared.Models;
using ArtToCart.Domain.Baskets;
using ArtToCart.Domain.Products;
using ArtToCart.Domain.Products.ValueObjects;

using FluentResults;

using FluentValidation;

using Mapster;

using MapsterMapper;

using MediatR;

using Microsoft.AspNetCore.Identity;

namespace ArtToCart.Application.Baskets.AddItemToBasket;

public record AddItemToBasketCommand(
    string Username,
    string Size,
    string CatalogItemId,
    int Quantity = 1) : IRequest<Result<AddItemToBasketResponse>>;

public class AddToBasketCommandValidator : AbstractValidator<AddItemToBasketCommand>
{
    public AddToBasketCommandValidator()
    {
        CascadeMode = CascadeMode.Stop;

        RuleFor(c => c.Username)
            .NotEmpty()
            .WithMessage("Username field is required");

        RuleFor(c => c.CatalogItemId)
            .NotEmpty()
            .WithMessage("Invalid catalog item id");

        RuleFor(c => c.Size)
            .NotEmpty()
            .WithMessage("Size is required");
    }
}

public class AddItemToBasketCommandHandler(
    IRepository<Basket> basketRepository,
    IMapper mapper,
    UserManager<ApplicationUser> userManager,
    IRepository<CatalogItem> productRepository)
    : IRequestHandler<AddItemToBasketCommand, Result<AddItemToBasketResponse>>
{
    public async Task<Result<AddItemToBasketResponse>> Handle(AddItemToBasketCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByNameAsync(request.Username);

        if (user == null)
        {
            return Result.Fail($"User with user name {request.Username} doesn't exist");
        }

        var basket = await basketRepository.FirstOrDefaultAsync(user.Id.ToString());

        if (basket == null)
        {
            basket = Basket.Create(user.Id.ToString());
            await basketRepository.AddAsync(basket);
        }

        var catalogItem = await productRepository.FirstOrDefaultAsync(request.CatalogItemId);

        if (catalogItem == null)
        {
            return Result.Fail("Bad Request: Adding non existent product to basket");
        }

        basket.AddItem(request.CatalogItemId, request.Size ,catalogItem.Price, request.Quantity);

        await basketRepository.UpdateAsync(basket);

        var result = mapper.Map<BasketDto>(basket);

        return new AddItemToBasketResponse(result);
    }
}
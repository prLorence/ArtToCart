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
    string CatalogItemId,
    decimal Price, int Quantity = 1) : IRequest<Result<AddItemToBasketResponse>>;

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

        RuleFor(c => c.Price)
            .NotEmpty()
            .WithMessage("Invalid price value");
    }
}

public class AddItemToBasketCommandHandler : IRequestHandler<AddItemToBasketCommand, Result<AddItemToBasketResponse>>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IRepository<Basket> _repository;
    private readonly IMapper _mapper;

    public AddItemToBasketCommandHandler(IRepository<Basket> repository, IMapper mapper, UserManager<ApplicationUser> userManager)
    {
        _repository = repository;
        _mapper = mapper;
        _userManager = userManager;
    }

    public async Task<Result<AddItemToBasketResponse>> Handle(AddItemToBasketCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(request.Username);

        if (user == null)
        {
            return Result.Fail($"User with user name {request.Username} doesn't exist");
        }

        var basket = await _repository.FirstOrDefaultAsync(user.Id.ToString());

        if (basket == null)
        {
            basket = Basket.Create(user.Id.ToString());
            await _repository.AddAsync(basket);
        }

        basket.AddItem(request.CatalogItemId, request.Price, request.Quantity);

        await _repository.UpdateAsync(basket);

        var result = _mapper.Map<BasketDto>(basket);

        return new AddItemToBasketResponse(result);
    }
}
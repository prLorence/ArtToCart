using ArtToCart.Application.Baskets.Shared;
using ArtToCart.Application.Shared.Interfaces;
using ArtToCart.Domain.Baskets;
using ArtToCart.Domain.Products;
using ArtToCart.Domain.Products.ValueObjects;

using FluentResults;

using FluentValidation;

using Mapster;

using MapsterMapper;

using MediatR;

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
    private readonly IRepository<Basket> _repository;
    private readonly IMapper _mapper;

    public AddItemToBasketCommandHandler(IRepository<Basket> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Result<AddItemToBasketResponse>> Handle(AddItemToBasketCommand request, CancellationToken cancellationToken)
    {
        var basket = await _repository.FirstOrDefaultAsync(request.Username);

        if (basket == null)
        {
            basket = Basket.Create(request.Username);
            await _repository.AddAsync(basket);
        }

        basket.AddItem(request.CatalogItemId, request.Price, request.Quantity);

        var result = _mapper.Map<BasketDto>(basket);

        await _repository.UpdateAsync(basket);

        return new AddItemToBasketResponse(result);
    }
}
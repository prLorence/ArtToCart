using ArtToCart.Application.Baskets.Shared;
using ArtToCart.Application.Shared.Interfaces;
using ArtToCart.Domain.Baskets;

using FluentResults;

using FluentValidation;

using MapsterMapper;

using MediatR;

namespace ArtToCart.Application.Baskets.UpdateBasketItems;

// string -> catalog item id
// int -> item new quantity
public record UpdateBasketItemsCommand(string BasketId, Dictionary<string, int> Quantities): IRequest<Result<UpdateBasketItemsResponse>>;

public class UpdateBasketItemsValidator : AbstractValidator<UpdateBasketItemsCommand>
{
    public UpdateBasketItemsValidator()
    {
        CascadeMode = CascadeMode.Stop;

        RuleFor(c => c.BasketId)
            .NotEmpty()
            .WithMessage("Basket id is required");

        RuleFor(c => c.Quantities)
            .NotEmpty()
            .WithMessage("New quantities are required");
    }
}

public class UpdateBasketItemsCommandHandler : IRequestHandler<UpdateBasketItemsCommand, Result<UpdateBasketItemsResponse>>
{
    private readonly IRepository<Basket> _repository;
    private readonly IMapper _mapper;

    public UpdateBasketItemsCommandHandler(IRepository<Basket> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Result<UpdateBasketItemsResponse>> Handle(UpdateBasketItemsCommand request, CancellationToken cancellationToken)
    {
        var basket = await _repository.FirstOrDefaultAsync(request.BasketId);

        if (basket == null)
        {
            return Result.Fail($"Basket does not exist");
        }

        foreach (var item in basket.Items)
        {
            if (request.Quantities.TryGetValue(item.Id.Value.ToString(), out var quantity))
            {
                item.SetQuantity(quantity);
            }
        }

        basket.RemoveEmptyItems();

        await _repository.UpdateAsync(basket);

        var result = _mapper.Map<BasketDto>(basket);

        return new UpdateBasketItemsResponse(result);
    }
}



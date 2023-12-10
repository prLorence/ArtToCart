using ArtToCart.Application.Baskets.RemoveItemFromBasket;
using ArtToCart.Application.Baskets.Shared;
using ArtToCart.Domain.Baskets;

using FluentResults;

using FluentValidation;

using MediatR;

namespace ArtToCart.Application.Baskets.UpdateBasketItemsCommand;

public record UpdateBasketItemsCommand(string BasketId, IEnumerable<BasketItem> Items): IRequest<Result<UpdateBasketItemsResponse>>;

public class UpdateBasketItemsValidator : AbstractValidator<UpdateBasketItemsCommand>
{
    public UpdateBasketItemsValidator()
    {
        CascadeMode = CascadeMode.Stop;

        RuleFor(c => c.BasketId)
            .NotEmpty()
            .WithMessage("Basket id is required");

        RuleFor(c => c.Items)
            .NotEmpty()
            .WithMessage("Updating items are required");
    }
}



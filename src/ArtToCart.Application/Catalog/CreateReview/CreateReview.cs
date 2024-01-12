using ArtToCart.Application.Catalog.Shared;
using ArtToCart.Application.Shared.Interfaces;
using ArtToCart.Domain.Products;
using ArtToCart.Domain.Products.Entities;
using ArtToCart.Domain.Products.ValueObjects;

using FluentResults;

using FluentValidation;

using MapsterMapper;

using MediatR;

namespace ArtToCart.Application.Catalog.CreateReview;

public record CreateReviewCommand(
    string CatalogItemId,
    string Value,
    string BuyerId,
    int Rating) : IRequest<Result<CreateReviewResponse>>;

public class CreateReviewCommandValidator : AbstractValidator<CreateReviewCommand>
{
    public CreateReviewCommandValidator()
    {
        CascadeMode = CascadeMode.Stop;

        RuleFor(c => c.CatalogItemId)
            .NotEmpty()
            .WithMessage("Catalog Item Id field is required");

        RuleFor(c => c.Value)
            .NotEmpty()
            .WithMessage("Review description field is required");

        RuleFor(c => c.BuyerId)
            .NotEmpty()
            .WithMessage("Buyer Id field is required");

        RuleFor(c => c.Rating)
            .NotEmpty()
            .WithMessage("Rating field field is required");
    }
}

public class CreateReviewCommandHandler(IRepository<CatalogItem> repository, IMapper mapper)
    : IRequestHandler<CreateReviewCommand, Result<CreateReviewResponse>>
{
    public async Task<Result<CreateReviewResponse>> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
    {
        var product = await repository.FirstOrDefaultAsync(request.CatalogItemId);

        if (product == null)
        {
            return Result.Fail("Adding review to an invalid product");
        }

        var review = new ItemReview(
            ItemReviewId.CreateUnique(),
            request.Value,
            request.Rating,
            CatalogItemId.CreateFrom(request.CatalogItemId),
            request.BuyerId);

        product.AddReviews(new List<ItemReview>{ review });

        product.AverageRating.AddNewRating(request.Rating);

        await repository.UpdateAsync(product);

        var result = mapper.Map<ProductDto>(product);

        return new CreateReviewResponse(result);
    }
}
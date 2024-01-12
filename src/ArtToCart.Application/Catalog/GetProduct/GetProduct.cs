using ArtToCart.Application.Catalog.GettingProducts;
using ArtToCart.Application.Catalog.Shared;
using ArtToCart.Application.Shared.Interfaces;
using ArtToCart.Domain.Products;

using FluentResults;

using FluentValidation;

using MapsterMapper;

using MediatR;

namespace ArtToCart.Application.Catalog.GetProduct;

public record GetProductQuery(string Id) : IRequest<Result<GetProductResponse>>;

public class GetProductQueryValidator : AbstractValidator<GetProductQuery>
{
    public GetProductQueryValidator()
    {
        CascadeMode = CascadeMode.Stop;

        RuleFor(q => q.Id)
            .NotEmpty()
            .WithMessage("Id field is required");
    }
}

public class GetProduct(IRepository<CatalogItem> productRepository, IMapper mapper)
    : IRequestHandler<GetProductQuery, Result<GetProductResponse>>
{
    public async Task<Result<GetProductResponse>> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        var products = await productRepository.FirstOrDefaultAsync(request.Id);

        if (products == null)
            return Result.Fail($"Product with an id of {request.Id} is not found");

        var result = mapper.Map<List<ProductDto>>(products);

        return new GetProductResponse(result);
    }
}
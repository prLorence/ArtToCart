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

public class GetProduct : IRequestHandler<GetProductQuery, Result<GetProductResponse>>
{
    private readonly IRepository<CatalogItem> _productRepository;
    private readonly IMapper _mapper;

    public GetProduct(IRepository<CatalogItem> productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<Result<GetProductResponse>> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        var products = await _productRepository.ListAsync(new[] {request.Id});


        var result = _mapper.Map<List<ProductDto>>(products);

        return new GetProductResponse(result);
    }
}
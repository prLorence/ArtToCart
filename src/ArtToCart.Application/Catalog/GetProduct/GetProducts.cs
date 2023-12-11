using System.Text.Json;
using System.Text.Json.Serialization;

using ArtToCart.Application.Catalog.Shared;
using ArtToCart.Application.Shared.Interfaces;
using ArtToCart.Domain.Products;
using ArtToCart.Domain.Products.Entities;

using FluentResults;

using FluentValidation;

using Mapster;

using MapsterMapper;

using MediatR;

using Microsoft.AspNetCore.Authentication;

namespace ArtToCart.Application.Catalog.GettingProducts;

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
        var products = await _productRepository.FirstOrDefaultAsync(request.Id);

        var result = _mapper.Map<ProductDto>(products);

        return new GetProductResponse(result);
    }
}
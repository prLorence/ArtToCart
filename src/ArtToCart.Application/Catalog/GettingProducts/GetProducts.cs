using System.Text.Json;
using System.Text.Json.Serialization;

using ArtToCart.Application.Catalog.Shared;
using ArtToCart.Application.Shared.Interfaces;
using ArtToCart.Domain.Products;
using ArtToCart.Domain.Products.Entities;

using FluentResults;

using Mapster;

using MapsterMapper;

using MediatR;

using Microsoft.AspNetCore.Authentication;

namespace ArtToCart.Application.Catalog.GettingProducts;


public record GetProductsQuery(string OrderBy) : IRequest<Result<GetProductsResponse>>;

public class GetProducts(IRepository<CatalogItem> productRepository, IMapper mapper)
    : IRequestHandler<GetProductsQuery, Result<GetProductsResponse>>
{
    public async Task<Result<GetProductsResponse>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await productRepository.GetAllAsync();

        products = products.OrderByDescending(p => p.AverageRating.Value);

        if (request.OrderBy.Equals("created"))
        {
            products = products.OrderByDescending(p => p.CreatedDateTime);
        }

        var result = mapper.Map<List<ProductDto>>(products);

        return new GetProductsResponse(result);
    }
}
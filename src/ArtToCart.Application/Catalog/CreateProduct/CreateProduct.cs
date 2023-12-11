using System.Net.Mime;

using ArtToCart.Application.Catalog.Shared;
using ArtToCart.Application.Shared.Interfaces;
using ArtToCart.Application.Shared.Models;
using ArtToCart.Domain.Products;
using ArtToCart.Domain.Products.Entities;
using ArtToCart.Domain.Products.ValueObjects;

using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

using FluentResults;

using FluentValidation;

using MapsterMapper;

using MediatR;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace ArtToCart.Application.Catalog.CreateProduct;

public record CreateProductCommand(
    string Name,
    decimal Price,
    string Description,
    string Size,
    string ArtistId,
    string CatalogType,
    List<IFormFile> Images): IRequest<Result<CreateProductResponse>>;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        CascadeMode = CascadeMode.Stop;

        RuleFor(c => c.Name)
            .NotEmpty()
            .WithMessage("Name field is required");

        RuleFor(c => c.Price)
            .NotEmpty()
            .WithMessage("Description field is required");

        RuleFor(c => c.Description)
            .NotEmpty()
            .WithMessage("Description field is required");

        RuleFor(c => c.Size)
            .NotEmpty()
            .WithMessage("Size field is required");

        RuleFor(c => c.ArtistId)
            .NotEmpty()
            .WithMessage("SellerId field is required");

        RuleFor(c => c.CatalogType)
            .NotEmpty()
            .WithMessage("Name field is required");

        RuleFor(c => c.Images)
            .NotEmpty()
            .WithMessage("Name field is required");
    }
}

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Result<CreateProductResponse>>
{
    private readonly IRepository<CatalogItem> _productRepository;
    private readonly IRepository<CatalogType> _catalogTypeRepository;
    private readonly BlobServiceClient _blobServiceClient;
    private readonly IConfiguration _configuration;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IMapper _mapper;

    public CreateProductCommandHandler(IRepository<CatalogItem> productRepository, IRepository<CatalogType> catalogTypeRepository, BlobServiceClient blobServiceClient, IConfiguration configuration, UserManager<ApplicationUser> userManager, IMapper mapper)
    {
        _productRepository = productRepository;
        _catalogTypeRepository = catalogTypeRepository;
        _blobServiceClient = blobServiceClient;
        _configuration = configuration;
        _userManager = userManager;
        _mapper = mapper;
    }

    public async Task<Result<CreateProductResponse>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var blobUrl = _configuration.GetConnectionString("AzureBlobStorageUri");
        var artist = await _userManager.FindByIdAsync(request.ArtistId);

        if (artist == null)
        {
            return Result.Fail("Unauthorized request");
        }

        var catalogType = await _catalogTypeRepository.FirstOrDefaultAsync(request.CatalogType);

        if (catalogType == null)
        {
            return Result.Fail("Invalid Catalog type");
        }

        // create product
        var product = CatalogItem.Create(
            request.Name,
            request.Price,
            request.Description,
            request.Size,
            request.ArtistId,
            AverageRating.CreateNew(),
            catalogType.Id,
            new List<ProductImage>(),
            new List<ItemReview>()
            );

        // upload images
        var productImages = new List<ProductImage>();

        var containerClient = _blobServiceClient.GetBlobContainerClient(artist.UserName);

        await containerClient.CreateIfNotExistsAsync(PublicAccessType.Blob, cancellationToken: cancellationToken);

        foreach (var image in request.Images)
        {
            var imageName = Guid.NewGuid().ToString();

            await containerClient.UploadBlobAsync($"{imageName}.png", image.OpenReadStream(), cancellationToken);

            productImages.Add(
                new ProductImage(
                    ProductImageId.CreateFrom(imageName),
                    $"{blobUrl}/{artist.UserName}/{imageName}.png",
                    isMain: true,
                    product.Id)
                );
        };

        product.AddProductImages(productImages);

        await _productRepository.AddAsync(product);

        var result = _mapper.Map<ProductDto>(product);

        return new CreateProductResponse(result);
    }
}
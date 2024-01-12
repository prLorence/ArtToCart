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
using Microsoft.Extensions.Logging;

namespace ArtToCart.Application.Catalog.CreateProduct;

public record CreateProductCommand(
    string Name,
    decimal Price,
    string Description,
    string Size,
    string ArtistId,
    List<IFormFile> Images,
    string? CatalogType = "design"): IRequest<Result<CreateProductResponse>>;

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
            .WithMessage("Price field is required");

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
            .WithMessage("Images field is required");
    }
}

public class CreateProductCommandHandler(
    IRepository<CatalogItem> productRepository,
    IRepository<CatalogType> catalogTypeRepository,
    BlobServiceClient blobServiceClient,
    IConfiguration configuration,
    UserManager<ApplicationUser> userManager,
    IMapper mapper,
    ILogger<CreateProductCommandHandler> logger)
    : IRequestHandler<CreateProductCommand, Result<CreateProductResponse>>
{
    public async Task<Result<CreateProductResponse>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var blobUrl = configuration.GetConnectionString("AzureBlobStorageUri");
        var artist = await userManager.FindByIdAsync(request.ArtistId);

        if (artist == null)
        {
            return Result.Fail("Unauthorized request");
        }

        var catalogType = await catalogTypeRepository.FirstOrDefaultAsync(request.CatalogType ?? "design");

        // create product
        var product = CatalogItem.Create(
            request.Name,
            request.Price,
            request.Description,
            request?.Size ?? "x",
             request.ArtistId,
            AverageRating.CreateNew(),
            catalogType.Id,
            new List<ProductImage>(),
            new List<ItemReview>()
            );

        // upload images
        var productImages = new List<ProductImage>();

        var containerClient = blobServiceClient.GetBlobContainerClient(artist.UserName);

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

        await productRepository.AddAsync(product);

        var result = mapper.Map<ProductDto>(product);

        logger.LogInformation("Product with ID: '{ProductId}' created", product.Id.Value.ToString());

        return new CreateProductResponse(result);
    }
}
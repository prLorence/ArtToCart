using ArtToCart.Domain.Products;
using ArtToCart.Domain.Products.Entities;
using ArtToCart.Domain.Products.ValueObjects;
using ArtToCart.Infrastructure.Shared;
using ArtToCart.Infrastructure.Shared.Persistance;

using Microsoft.EntityFrameworkCore;

namespace ArtToCart.Infrastructure.Data;

public class CatalogDataSeeder : IDataSeeder
{
    private readonly ArtToCartIdentityDbContext _context;
    private const string DesignCatalogId = "bc667dc3-0fdd-43cd-8ac7-87dd92a56467";
    private const string BasicCatalogId = "2abe29b6-3b19-44d4-94da-b45eb08b7e59";

    public CatalogDataSeeder(ArtToCartIdentityDbContext context)
    {
        _context = context;
    }
    public async Task SeedAllAsync()
    {
        await SeedBasicCatalog();
        await SeedDesignCatalog();
    }

    public async Task SeedBasicCatalog()
    {
        CatalogType basicCatalog = CatalogType.Create(BasicCatalogId,"Basic Catalog");

        if (_context.CatalogTypes.All(ci => ci.Type != basicCatalog.Type))
        {
            await _context.AddAsync(basicCatalog);
        }

        CatalogItem item1 = CatalogItem.Create(
            "Basic Product 1",
            150.00m,
            "Test Description",
            "L",
            "sellerId",
            AverageRating.CreateNew(),
            basicCatalog.Id,
            new List<ProductImage>(),
            new List<ItemReview>());

        item1.AddProductImages( new List<ProductImage> {
            new(
            ProductImageId.CreateUnique(),
            "https://atcblobstore.blob.core.windows.net/product-images/P_Black.png",
            true,
            item1.Id)
        });

        item1.AddReviews(new List<ItemReview>
        {
            new(ItemReviewId.CreateUnique(),
                "Great Product!",
                item1.Id,
                "buyerId")
        });


        CatalogItem item2 = CatalogItem.Create(
            "Basic Product 2",
            150.00m,
            "Test Description",
            "L",
            "sellerId",
            AverageRating.CreateNew(),
            basicCatalog.Id,
                new List<ProductImage>(),
            new List<ItemReview>());

        item2.AddProductImages( new List<ProductImage> {
            new(
            ProductImageId.CreateUnique(),
            "https://atcblobstore.blob.core.windows.net/product-images/P_White.png",
            true,
            item2.Id)
        });

        item2.AddReviews(new List<ItemReview>
        {
            new(ItemReviewId.CreateUnique(),
                "Great Product!",
                item2.Id,
                "buyerId")
        });

        var seedData = new List<CatalogItem> { item1, item2 };

        foreach (var data in seedData.Where(
                     data => !_context.CatalogItems.Any(ci => ci.Name == data.Name)))
        {
            await _context.AddAsync(data);

            await _context.SaveChangesAsync();
        }
    }
    public async Task SeedDesignCatalog()
    {
        CatalogType designCatalog = CatalogType.Create(DesignCatalogId,"Design Catalog");

        if (_context.CatalogTypes.All(ci => ci.Type != designCatalog.Type))
        {
            await _context.AddAsync(designCatalog);
        }
        CatalogItem item1 = CatalogItem.Create(
            "Test Product 1",
            150.00m,
            "Test Description",
            "L",
            "sellerId",
            AverageRating.CreateNew(),
            designCatalog.Id,
            new List<ProductImage>(),
            new List<ItemReview>());

        item1.AddProductImages( new List<ProductImage> {
            new(
            ProductImageId.CreateUnique(),
            "https://atcblobstore.blob.core.windows.net/product-images/D1_Black.png",
            true,
            item1.Id)
        });

        item1.AddReviews(new List<ItemReview>
        {
            new(ItemReviewId.CreateUnique(),
                "Great Product!",
                item1.Id,
                "buyerId")
        });

        CatalogItem item2 = CatalogItem.Create(
            "Test Product 2",
            150.00m,
            "Test Description",
            "L",
            "sellerId",
            AverageRating.CreateNew(),
            designCatalog.Id,
            new List<ProductImage>(),
            new List<ItemReview>());

        item2.AddProductImages( new List<ProductImage> {
            new(
            ProductImageId.CreateUnique(),
            "https://atcblobstore.blob.core.windows.net/product-images/D1_White.png",
            true,
            item2.Id)
        });

        item2.AddReviews(new List<ItemReview>
        {
            new(ItemReviewId.CreateUnique(),
                "Great Product!",
                item2.Id,
                "buyerId")
        });

        CatalogItem item3 = CatalogItem.Create(
            "Test Product 3",
            150.00m,
            "Test Description",
            "L",
            "sellerId",
            AverageRating.CreateNew(),
            designCatalog.Id,
            new List<ProductImage>(),
            new List<ItemReview>());

        item3.AddProductImages( new List<ProductImage> {
            new(
            ProductImageId.CreateUnique(),
            "https://atcblobstore.blob.core.windows.net/product-images/D2_White.png",
            true,
            item3.Id)
        });

        item3.AddReviews(new List<ItemReview>
        {
            new(ItemReviewId.CreateUnique(),
                "Great Product!",
                item3.Id,
                "buyerId")
        });

        CatalogItem item4 = CatalogItem.Create(
            "Test Product 4",
            150.00m,
            "Test Description",
            "L",
            "sellerId",
            AverageRating.CreateNew(),
            designCatalog.Id,
            new List<ProductImage>(),
            new List<ItemReview>());

        item4.AddProductImages( new List<ProductImage> {
            new(
            ProductImageId.CreateUnique(),
            "https://atcblobstore.blob.core.windows.net/product-images/D2_Black.png",
            true,
            item4.Id)
        });

        item4.AddReviews(new List<ItemReview>
        {
            new(
                ItemReviewId.CreateUnique(),
                "Great Product!",
                item4.Id,
                "buyerId")
        });

        CatalogItem item5 = CatalogItem.Create(
            "Test Product 5",
            150.00m,
            "Test Description",
            "L",
            "sellerId",
            AverageRating.CreateNew(),
            designCatalog.Id,
            new List<ProductImage>(),
            new List<ItemReview>());

        item5.AddProductImages( new List<ProductImage> {
            new(
            ProductImageId.CreateUnique(),
            "https://atcblobstore.blob.core.windows.net/product-images/D3_White.png",
            true,
            item5.Id)
        });

        item5.AddReviews(new List<ItemReview>
        {
            new(ItemReviewId.CreateUnique(),
                "Great Product!",
                item5.Id,
                "buyerId")
        });

        CatalogItem item6 = CatalogItem.Create(
            "Test Product 6",
            150.00m,
            "Test Description",
            "L",
            "sellerId",
            AverageRating.CreateNew(),
            designCatalog.Id,
            new List<ProductImage>(),
            new List<ItemReview>());

        item6.AddProductImages( new List<ProductImage> {
            new(
            ProductImageId.CreateUnique(),
            "https://atcblobstore.blob.core.windows.net/product-images/D3_Black.png",
            true,
            item6.Id)
        });

        item6.AddReviews(new List<ItemReview>
        {
            new(ItemReviewId.CreateUnique(),
                "Great Product!",
                item6.Id,
                "buyerId")
        });

        var seedData = new List<CatalogItem> { item1, item2, item3, item4, item5, item6 };

        foreach (var data in seedData.Where(
                     data => !_context.CatalogItems.Any(ci => ci.Name == data.Name)))
        {
            await _context.AddAsync(data);

            await _context.SaveChangesAsync();
        }
    }
}
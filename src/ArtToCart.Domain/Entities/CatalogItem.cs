using ArtToCart.Domain.Common;
using ArtToCart.Domain.Entities.ValueObjects;

namespace ArtToCart.Domain.Entities;

public class CatalogItem : BaseEntity<ProductId>
{
    public string Name { get; set;  }
    public decimal Price { get; set; }
    public string Description { get; set; }
    public string SellerId { get; set; }

    public string ProductPhotoUri { get; set; }

    private CatalogItem(ProductId id,
        string name,
        decimal price,
        string description,
        string sellerId,
        string productPhotoUri) : base(id)
    {
        Name = name;
        Price = price;
        Description = description;
        SellerId = sellerId;
        ProductPhotoUri = productPhotoUri;
    }
#pragma warning disable CS8618 // Required by Entity Framework
    private CatalogItem(string productPhotoUri)
    {
        ProductPhotoUri = productPhotoUri;
    }
#pragma warning disable CS8618

    public CatalogItem Create(string name, decimal price, string description, string sellerId, string productPhotoUri)
    {
        return new(
            ProductId.CreateUnique(),
            name,
            price,
            description,
            sellerId,
            productPhotoUri
        );
    }
}
using ArtToCart.Domain.Common;
using ArtToCart.Domain.Entities.ValueObjects;
using ArtToCart.Domain.Interfaces;

namespace ArtToCart.Domain.Entities;

public class CatalogItem : BaseEntity<CatalogItemId>, IAggregateRoot
{
    public string Name { get; set;  }
    public decimal Price { get; set; }
    public string Description { get; set; }
    public string SellerId { get; set; }
    public string ProductPhotoUri { get; set; }

    private CatalogItem(CatalogItemId id,
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
    private CatalogItem()
    {
    }
#pragma warning disable CS8618

    public CatalogItem Create(string name, decimal price, string description, string sellerId, string productPhotoUri)
    {
        return new(
            CatalogItemId.CreateUnique(),
            name,
            price,
            description,
            sellerId,
            productPhotoUri
        );
    }
}
using ArtToCart.Core.Domain;
using ArtToCart.Domain.Common;
using ArtToCart.Domain.Products.ValueObjects;

namespace ArtToCart.Domain.Products;

public class CatalogItem : BaseEntity<CatalogItemId>, IAggregateRoot
{
    public string Name { get; private set;  }
    public decimal Price { get; private set; }
    public string Description { get; private set; }
    public string SellerId { get; private set; }
    public string ProductPhotoUri { get; private set; }
    public int CatalogTypeId { get; private set; }
    public CatalogType? CatalogType { get; private set; }

    private CatalogItem(CatalogItemId id,
        string name,
        decimal price,
        string description,
        string sellerId,
        string productPhotoUri,
        int catalogTypeId) : base(id)
    {
        Name = name;
        Price = price;
        Description = description;
        SellerId = sellerId;
        ProductPhotoUri = productPhotoUri;
        CatalogTypeId = catalogTypeId;
    }
#pragma warning disable CS8618 // Required by Entity Framework
    private CatalogItem()
    {
    }
#pragma warning disable CS8618

    public CatalogItem Create(string name,
        decimal price,
        string description,
        string sellerId,
        string productPhotoUri,
        int catalogTypeId)
    {
        return new(
            CatalogItemId.CreateUnique(),
            name,
            price,
            description,
            sellerId,
            productPhotoUri,
            catalogTypeId
        );
    }
}
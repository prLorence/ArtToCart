using ArtToCart.Core.Domain;
using ArtToCart.Domain.Common;
using ArtToCart.Domain.Products.Entities;
using ArtToCart.Domain.Products.ValueObjects;

namespace ArtToCart.Domain.Products;

public class CatalogItem : BaseEntity<CatalogItemId>, IAggregateRoot
{
    private List<ProductImage> _images = new();
    public string Name { get; private set;  }
    public decimal Price { get; private set; }
    public string Description { get; private set; }
    public string Size { get; private set;  }
    public string SellerId { get; private set; }
    public CatalogTypeId CatalogTypeId { get; private set; }
    public CatalogType? CatalogType { get; private set; }
    public IReadOnlyList<ProductImage> Images => _images.AsReadOnly();

    private CatalogItem(CatalogItemId id,
        string name,
        decimal price,
        string description,
        string size,
        string sellerId,
        CatalogTypeId catalogTypeId) : base(id)
    {
        Name = name;
        Price = price;
        Description = description;
        Size = size;
        SellerId = sellerId;
        CatalogTypeId = catalogTypeId;
    }
#pragma warning disable CS8618 // Required by Entity Framework
    private CatalogItem()
    {
    }
#pragma warning disable CS8618

    public static CatalogItem Create(string name,
        decimal price,
        string description,
        string size,
        string sellerId,
        CatalogTypeId catalogTypeId,
        IList<ProductImage> productImages)
    {
        CatalogItem newProduct = new(
            CatalogItemId.CreateUnique(),
            name,
            price,
            description,
            size,
            sellerId,
            catalogTypeId
        );

        newProduct.AddProductImages(productImages);

        return newProduct;
    }

    public void AddProductImages(IList<ProductImage>? productImages)
    {
        if (productImages is null)
        {
            _images = null!;
            return;
        }

        _images.AddRange(productImages);
    }
}
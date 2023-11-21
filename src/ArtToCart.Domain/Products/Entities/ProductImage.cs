using ArtToCart.Core.Domain;
using ArtToCart.Domain.Common;
using ArtToCart.Domain.Products.ValueObjects;

namespace ArtToCart.Domain.Products.Entities;

public class ProductImage : BaseEntity<ProductImageId>
{
    private ProductImage()
    {
    }
    public ProductImage(ProductImageId id, string imageUrl, bool isMain, CatalogItemId catalogItemId) : base(id)
    {
        SetImageUrl(imageUrl);
        SetIsMain(isMain);
        CatalogItemId = catalogItemId;
    }
    public string ImageUrl { get; private set; } = default!;
    public bool IsMain { get; private set; }
    public CatalogItem CatalogItem{ get; private set; } = null!;
    public CatalogItemId CatalogItemId { get; private set; }

    public void SetIsMain(bool isMain) => IsMain = isMain;
    public void SetImageUrl(string url) => ImageUrl = url;
}
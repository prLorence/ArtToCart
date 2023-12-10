using ArtToCart.Domain.Products.ValueObjects;

namespace ArtToCart.Domain.Orders;

public class CatalogItemOrdered
{
    public CatalogItemOrdered(CatalogItemId catalogItemId, string productName, string pictureUri)
    {
        CatalogItemId = catalogItemId;
        ProductName = productName;
        PictureUri = pictureUri;
    }

    #pragma warning disable CS8618 // Required by Entity Framework
    private CatalogItemOrdered() {}

    public CatalogItemId CatalogItemId { get; private set; }
    public string ProductName { get; private set; }
    public string PictureUri { get; private set; }
}
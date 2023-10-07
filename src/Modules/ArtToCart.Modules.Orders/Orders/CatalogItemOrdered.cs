namespace ArtToCart.Modules.Orders.Orders;

public class CatalogItemOrdered
{
    public CatalogItemOrdered(int catalogItemId, string productName, string pictureUri)
    {
        CatalogItemId = catalogItemId;
        ProductName = productName;
        PictureUri = pictureUri;
    }

    #pragma warning disable CS8618 // Required by Entity Framework
    private CatalogItemOrdered() {}

    public int CatalogItemId { get; private set; }
    public string ProductName { get; private set; }
    public string PictureUri { get; private set; }
}
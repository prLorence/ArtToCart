using ArtToCart.Core.Domain;
using ArtToCart.Modules.Customers.Baskets.ValueObjects;

namespace ArtToCart.Modules.Customers.Baskets;

public class BasketItem : BaseEntity<BasketItemId>
{
    public string ItemPhotoUri { get; private set; }
    public int Quantity { get; private set; }
    public decimal UnitPrice { get; private set; }
    public string CatalogItemId { get; private set; }
    public string BasketId { get; private set; }

    private BasketItem(BasketItemId basketItemId,
        string catalogItemId,
        decimal unitPrice,
        int quantity) : base(basketItemId)
    {
        Quantity = quantity;
        UnitPrice = unitPrice;
        CatalogItemId = catalogItemId;
    }

    public static BasketItem Create(string catalogItemId, decimal unitPrice, int quantity)
    {
        return new BasketItem(BasketItemId.CreateUnique(),
            catalogItemId,
            unitPrice,
            quantity);
    }

    public void AddQuantity(int quantity)
    {
        Quantity += quantity;
    }

    public void SetQuantity(int quantity)
    {
        Quantity = quantity;
    }

}
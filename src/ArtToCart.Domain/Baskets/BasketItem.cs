using ArtToCart.Domain.Baskets.ValueObjects;
using ArtToCart.Domain.Common;

namespace ArtToCart.Domain.Baskets;

public class BasketItem : BaseEntity<BasketItemId>
{
    public int Quantity { get; private set; }
    public decimal UnitPrice { get; private set; }
    public string CatalogItemId { get; private set; }
    public string Size { get; private set; }
    public BasketId BasketId { get; set; }
    public Basket Basket { get; set; }

    // required by EF Core
    protected BasketItem()
    {
    }


    private BasketItem(BasketItemId basketItemId,
        string catalogItemId,
        string size,
        decimal unitPrice,
        int quantity) : base(basketItemId)
    {
        Size = size;
        Quantity = quantity;
        UnitPrice = unitPrice;
        CatalogItemId = catalogItemId;
    }

    public static BasketItem Create(string catalogItemId, string size, decimal unitPrice, int quantity)
    {
        return new BasketItem(BasketItemId.CreateUnique(),
            catalogItemId,
            size,
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
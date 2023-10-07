using ArtToCart.Core.Domain;
using ArtToCart.Modules.Customers.Baskets.ValueObjects;

namespace ArtToCart.Modules.Customers.Baskets;

public class Basket : BaseEntity<BasketId>, IAggregateRoot
{
    public string BuyerId { get; set; }
    private readonly List<BasketItem> _basketItems = new();
    public IReadOnlyCollection<BasketItem> BasketItems => _basketItems.AsReadOnly();
    public int TotalItemCount => _basketItems.Sum(i => i.Quantity);

    private Basket()
    {
    }

    private Basket(BasketId basketId, string buyerId) : base(basketId)
    {
        BuyerId = buyerId;
    }

    public static Basket Create(string buyerId)
    {
        return new Basket(BasketId.CreateUnique(), buyerId);
    }

    public void AddItem(string catalogItemId, decimal unitPrice, int quantity = 1)
    {
        if (BasketItems.Any(i => i.CatalogItemId != catalogItemId))
        {
            _basketItems.Add(BasketItem.Create(catalogItemId, unitPrice, quantity));
            return;
        }

        var existingItem = BasketItems.First(i => i.CatalogItemId == catalogItemId);
        existingItem.AddQuantity(quantity);
    }

    public void RemoveEmptyItems()
    {
        _basketItems.RemoveAll(i => i.Quantity == 0);
    }
}
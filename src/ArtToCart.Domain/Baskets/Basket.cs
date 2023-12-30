using ArtToCart.Core.Domain;
using ArtToCart.Domain.Baskets.ValueObjects;
using ArtToCart.Domain.Common;

namespace ArtToCart.Domain.Baskets;

public class Basket : BaseEntity<BasketId>, IAggregateRoot
{
    public string BuyerId { get; set; }
    private readonly List<BasketItem> _items = new();
    public IReadOnlyCollection<BasketItem> Items => _items.AsReadOnly();
    public int TotalItemCount => _items.Sum(i => i.Quantity);

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

    public void AddItem(string catalogItemId, string size, decimal unitPrice, int quantity = 1)
    {
        if (!Items.Any(i => i.CatalogItemId == catalogItemId))
        {
            _items.Add(BasketItem.Create(catalogItemId, size, unitPrice, quantity));
            return;
        }

        var existingItem = Items.FirstOrDefault(i => i.CatalogItemId == catalogItemId);

        existingItem!.AddQuantity(quantity);
    }

    public void RemoveEmptyItems()
    {
        _items.RemoveAll(i => i.Quantity == 0);
    }

    public void RemoveCheckedOutItems()
    {
        _items.Clear();
    }
}
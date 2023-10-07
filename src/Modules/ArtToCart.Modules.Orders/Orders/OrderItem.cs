using ArtToCart.Core.Domain;
using ArtToCart.Modules.Orders.Orders.ValueObjects;

namespace ArtToCart.Modules.Orders.Orders;

public class OrderItem : BaseEntity<OrderItemId>
{
    public CatalogItemOrdered ItemOrdered { get; private set; }
    public decimal UnitPrice { get; private set; }
    public int Units { get; private set; }

    #pragma warning disable CS8618 // Required by Entity Framework
    private OrderItem() {}

    public OrderItem(OrderItemId itemId, CatalogItemOrdered itemOrdered, decimal unitPrice, int units) : base(itemId)
    {
        ItemOrdered = itemOrdered;
        UnitPrice = unitPrice;
        Units = units;
    }
}
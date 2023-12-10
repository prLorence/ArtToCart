using ArtToCart.Core.Domain;
using ArtToCart.Domain.Common;
using ArtToCart.Domain.Orders.ValueObjects;

namespace ArtToCart.Domain.Orders;

public class Order : BaseEntity<OrderId>, IAggregateRoot
{
    #pragma warning disable CS8618 // Required by Entity Framework
    private Order() {}

    private Order(OrderId id, string buyerId, Address shipToAddress, List<OrderItem> items) : base(id)
    {
        BuyerId = buyerId;
        ShipToAddress = shipToAddress;
        _orderItems = items;
    }

    public decimal Total => _orderItems.Sum(item => item.UnitPrice * item.Units);
    public string BuyerId { get; private set; }
    public DateTimeOffset OrderDate { get; private set; } = DateTimeOffset.Now;
    public Address ShipToAddress { get; private set; }

    private readonly List<OrderItem> _orderItems = new();
    public IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();

    public static Order Create(string buyerId, Address shippingAddress, List<OrderItem> items)
    {
        return new(OrderId.CreateUnique(),
            buyerId,
            shippingAddress,
            items
        );
    }
}
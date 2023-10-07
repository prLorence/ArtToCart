namespace ArtToCart.Modules.Orders.Orders;

public class Order
{
    #pragma warning disable CS8618 // Required by Entity Framework
    private Order() {}

    public Order(string buyerId, Address shipToAddress, List<OrderItem> items)
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

}
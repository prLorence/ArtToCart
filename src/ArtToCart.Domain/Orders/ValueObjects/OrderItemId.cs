using ArtToCart.Core.Domain;

namespace ArtToCart.Domain.Orders.ValueObjects;

public class OrderItemId : ValueObject
{
    public Guid Value { get; private set; }

    public OrderItemId(Guid value)
    {
        Value = value;
    }
    public static OrderItemId CreateUnique()
    {
        return new OrderItemId(Guid.NewGuid());
    }

    public static OrderItemId CreateFrom(string id)
    {
        return new(Guid.Parse(id));
    }

    public static OrderItemId CreateFrom(Guid id)
    {
        return new(id);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
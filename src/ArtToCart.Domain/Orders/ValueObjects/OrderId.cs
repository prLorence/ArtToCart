using ArtToCart.Core.Domain;

namespace ArtToCart.Domain.Orders.ValueObjects;

public class OrderId : ValueObject
{
    public Guid Value { get; private set; }

    public OrderId(Guid value)
    {
        Value = value;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static OrderId CreateUnique()
    {
        return new OrderId(Guid.NewGuid());
    }

    public static OrderId CreateFrom(string id)
    {
        return new(Guid.Parse(id));
    }

    public static OrderId CreateFrom(Guid id)
    {
        return new(id);
    }
}
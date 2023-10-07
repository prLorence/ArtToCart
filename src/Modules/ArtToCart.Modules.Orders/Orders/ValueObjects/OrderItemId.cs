using ArtToCart.Core.Domain;

namespace ArtToCart.Modules.Orders.Orders.ValueObjects;

public class OrderItemId : ValueObject
{
    public string Value { get; private set; }

    public OrderItemId(string value)
    {
        Value = value;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
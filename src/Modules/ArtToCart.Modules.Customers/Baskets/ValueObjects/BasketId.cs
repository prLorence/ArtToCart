using ArtToCart.Core.Domain;

namespace ArtToCart.Modules.Customers.Baskets.ValueObjects;

public class BasketId : ValueObject
{
    public Guid Value { get; private set; }

    private BasketId(Guid value)
    {
        Value = value;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static BasketId CreateUnique()
    {
        return new BasketId(Guid.NewGuid());
    }

    public static BasketId CreateFrom(string id)
    {
        return new(Guid.Parse(id));
    }
}
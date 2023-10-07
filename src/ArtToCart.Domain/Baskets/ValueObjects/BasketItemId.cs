using ArtToCart.Core.Domain;

namespace ArtToCart.Domain.Baskets.ValueObjects;

public class BasketItemId : ValueObject
{
    public Guid Value { get; set; }

    private BasketItemId(Guid value)
    {
        Value = value;
    }

    public static BasketItemId CreateUnique()
    {
        return new(Guid.NewGuid());
    }

    public static BasketItemId CreateFrom(string id)
    {
        return new(Guid.Parse(id));
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
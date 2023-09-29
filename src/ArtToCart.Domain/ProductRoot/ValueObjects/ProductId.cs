using ArtToCart.Domain.Common;

namespace ArtToCart.Domain.ProductRoot.ValueObjects;

public class ProductId : ValueObject
{
    public Guid Value { get; protected set; }

    private ProductId(Guid value)
    {
        Value = value;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static ProductId CreateUnique()
    {
        return new ProductId(Guid.NewGuid());
    }

    public static ProductId CreateFrom(Guid guid)
    {
        return new ProductId(guid);
    }
}
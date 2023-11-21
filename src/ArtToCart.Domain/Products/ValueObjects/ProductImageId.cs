using ArtToCart.Core.Domain;

namespace ArtToCart.Domain.Products.ValueObjects;

public class ProductImageId : ValueObject
{
    public Guid Value { get; private set; }

    private ProductImageId(Guid value)
    {
        Value = value;
    }
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static ProductImageId CreateUnique()
    {
        return new(Guid.NewGuid());
    }

    public static ProductImageId CreateFrom(string id)
    {
        return new(Guid.Parse(id));
    }

    public static ProductImageId CreateFrom(Guid id)
    {
        return new(id);
    }
}
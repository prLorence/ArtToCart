using ArtToCart.Core.Domain;

namespace ArtToCart.Domain.Products.ValueObjects;

public class CatalogTypeId : ValueObject
{
    public Guid Value { get; private set; }

    private CatalogTypeId(Guid value)
    {
        Value = value;
    }
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static CatalogTypeId CreateUnique()
    {
        return new(Guid.NewGuid());
    }

    public static CatalogTypeId CreateFrom(string id)
    {
        return new(Guid.Parse(id));
    }

}
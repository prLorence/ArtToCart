using ArtToCart.Domain.Common;

namespace ArtToCart.Domain.Entities.ValueObjects;

public class CatalogItemId : ValueObject
{
    public Guid Value { get; private set; }

    private CatalogItemId(Guid value)
    {
        Value = value;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static CatalogItemId CreateUnique()
    {
        return new CatalogItemId(Guid.NewGuid());
    }

    public static CatalogItemId CreateFrom(string id)
    {
        return new CatalogItemId(Guid.Parse(id));
    }
}
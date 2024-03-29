
using ArtToCart.Core.Domain;

namespace ArtToCart.Domain.Products.ValueObjects;

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

    public static CatalogItemId CreateFrom(Guid id)
    {
        return new CatalogItemId(id);
    }
}
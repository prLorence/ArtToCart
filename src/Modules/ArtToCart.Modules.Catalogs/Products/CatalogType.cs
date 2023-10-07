using ArtToCart.Core.Domain;
using ArtToCart.Modules.Catalogs.Products.ValueObjects;

namespace ArtToCart.Modules.Catalogs.Products;

public class CatalogType : BaseEntity<CatalogTypeId>, IAggregateRoot
{
    public string Type { get; private set; }

#pragma warning disable CS8618 // Required by Entity Framework
    private CatalogType()
    {
    }
#pragma warning restore CS8618 // Required by Entity Framework

    private CatalogType(CatalogTypeId id, string type) : base(id)
    {
        Type = type;
    }

    public CatalogType Create(string brand)
    {
        return new(
            CatalogTypeId.CreateUnique(),
            brand
        );
    }
}
using ArtToCart.Core.Domain;
using ArtToCart.Domain.Common;
using ArtToCart.Domain.Products.ValueObjects;

namespace ArtToCart.Domain.Products;

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

    public static CatalogType Create(string id, string brand)
    {
        return new(
            CatalogTypeId.CreateFrom(id),
            brand
        );
    }
}
using ArtToCart.Core.Domain;

namespace ArtToCart.Domain.Common;

public class BaseAuditableEntity<T> : BaseEntity<T>
    where T: ValueObject
{
    public DateTimeOffset Created { get; set; }

    public string? CreatedBy { get; set; }

    public DateTimeOffset LastModified { get; set; }

    public string? LastModifiedBy { get; set; }
}
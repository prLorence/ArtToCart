using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.AspNetCore.Http;

namespace ArtToCart.Domain.Common;

public abstract class BaseEntity<T>
    where T : ValueObject
{
    protected T Id { get; init; }

    private readonly List<BaseEvent> _domainEvents = new();

    protected BaseEntity()
    {

    }
    protected BaseEntity(T id)
    {
        Id = id;
    }

    [NotMapped]
    public IReadOnlyCollection<BaseEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void AddDomainEvent(BaseEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void RemoveDomainEvent(BaseEvent domainEvent)
    {
        _domainEvents.Remove(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}

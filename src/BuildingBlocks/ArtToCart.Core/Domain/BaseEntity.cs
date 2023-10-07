using System.ComponentModel.DataAnnotations.Schema;

namespace ArtToCart.Core.Domain;

public abstract class BaseEntity<T>
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

using Domain.Entities;
using Domain.Events;

namespace Domain.Aggregates;

public interface IAggregateRoot<TId> : IEntity<TId>
    where TId : struct, IEquatable<TId>
{
    public IEnumerable<IDomainEvent> DomainEvents { get; }

    public void AddDomainEvent(IDomainEvent domainEvent);

    public void ClearDomainEvents();
}
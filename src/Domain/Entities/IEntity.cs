namespace Domain.Entities;

public interface IEntity<TId> : ITrackedEntity where TId : struct, IEquatable<TId>
{
    public TId Id { get; set; }

    public static IEqualityComparer<Entity<TId>> IdEqualityComparer =>
        EqualityComparer<Entity<TId>>.Create((x, y) =>
            x is null
                ? y is null
                : y is not null
                  && x.GetType() == y.GetType()
                  && x.Id.Equals(y.Id));
}
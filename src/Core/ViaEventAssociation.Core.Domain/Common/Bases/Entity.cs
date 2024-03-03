namespace ViaEventAssociation.Core.Domain;

public abstract class Entity<TId>
{
    public TId Id { get; }

    public Entity(TId id)
    {
        Id = id;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (obj.GetType() != GetType()) return false;

        Entity<TId> otherEntity = (Entity<TId>) obj;
        return otherEntity.Id!.Equals(Id);
    }


    public override int GetHashCode()
    {
        return Id!.GetHashCode();
    }

    public static bool operator ==(Entity<TId>? left, Entity<TId>? right)
    {
        return left is not null && right is not null && Equals(right, left);
    }


    public static bool operator !=(Entity<TId>? left, Entity<TId>? right)
    {
        return left is not null && right is not null && !Equals(right, left);
    }
}


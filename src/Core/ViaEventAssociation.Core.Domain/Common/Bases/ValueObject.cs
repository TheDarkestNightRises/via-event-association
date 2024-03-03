namespace ViaEventAssociation.Core.Domain;

public abstract class ValueObject
{
    public abstract IEnumerable<object> GetEqualityObjects();

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (obj.GetType() != GetType()) return false;

        return ValuesAreEquals((ValueObject)obj);
    }

    private bool ValuesAreEquals(ValueObject other)
    {
        return GetEqualityObjects().SequenceEqual(other.GetEqualityObjects());
    }

    public override int GetHashCode()
    {
        return GetEqualityObjects()
                .Aggregate(default(int), HashCode.Combine);
    }

    public static bool operator ==(ValueObject? left, ValueObject? right)
    {
        return left is not null && right is not null && Equals(right, left);
    }


    public static bool operator !=(ValueObject? left, ValueObject? right)
    {
        return left is not null && right is not null && !Equals(right, left);
    }

}

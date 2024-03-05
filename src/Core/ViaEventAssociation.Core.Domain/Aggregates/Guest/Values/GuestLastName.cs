using ViaEventAssociation.Core.Domain.Aggregates.Event.EventErrors;
using ViaEventAssociation.Core.Tools.OperationResult;
using Void = ViaEventAssociation.Core.Tools.OperationResult.Void;

namespace ViaEventAssociation.Core.Domain.Aggregates.Guest.Values;

public class GuestLastName : ValueObject
{
    internal string LastName { get; }

    internal GuestLastName(string lastName)
    {
        LastName = lastName;
    }

    public static Result<GuestLastName> Create(string? lastName)
    {
        var validationResult = Validate(lastName);
        
        return validationResult.Match<Result<GuestLastName>>(
            onPayLoad: _ => new GuestLastName(lastName!),
            onError: errors => errors
        );
    }

    private static Result<Void> Validate(string? lastName)
    {
        if (lastName is null)
        {
            return EventAggregateErrors.EventDescriptionCantBeNull;
        }
        
        return new Void();
    }

    public override IEnumerable<object> GetEqualityObjects()
    {
        yield return LastName;
    }

    public static explicit operator string(GuestLastName guestLastName)
    {
        return guestLastName.LastName;
    }

}
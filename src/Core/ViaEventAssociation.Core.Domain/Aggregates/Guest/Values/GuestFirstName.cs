using ViaEventAssociation.Core.Domain.Aggregates.Event.EventErrors;
using ViaEventAssociation.Core.Tools.OperationResult;
using Void = ViaEventAssociation.Core.Tools.OperationResult.Void;

namespace ViaEventAssociation.Core.Domain.Aggregates.Guest.Values;

public class GuestFirstName : ValueObject
{
    internal string FirstName { get; }

    internal GuestFirstName(string firstName)
    {
        FirstName = firstName;
    }

    public static Result<GuestFirstName> Create(string? firstName)
    {
        var validationResult = Validate(firstName);
        
        return validationResult.Match<Result<GuestFirstName>>(
            onPayLoad: _ => new GuestFirstName(firstName!),
            onError: errors => errors
        );
    }

    private static Result<Void> Validate(string? firstName)
    {
        if (firstName is null)
        {
            return EventAggregateErrors.EventDescriptionCantBeNull;
        }
        
        return new Void();
    }

    public override IEnumerable<object> GetEqualityObjects()
    {
        yield return FirstName;
    }

    public static explicit operator string(GuestFirstName guestFirstName)
    {
        return guestFirstName.FirstName;
    }

}
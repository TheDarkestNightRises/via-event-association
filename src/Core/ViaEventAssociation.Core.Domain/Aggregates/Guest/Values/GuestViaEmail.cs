using ViaEventAssociation.Core.Domain.Aggregates.Guest.GuestErrors;
using ViaEventAssociation.Core.Tools.OperationResult;
using Void = ViaEventAssociation.Core.Tools.OperationResult.Void;

namespace ViaEventAssociation.Core.Domain.Aggregates.Guest.Values;

public class GuestViaEmail : ValueObject
{
    internal string ViaEmail { get; }

    internal GuestViaEmail(string viaEmail)
    {
        ViaEmail = viaEmail;
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
            return GuestAggregateErrors.EventDescriptionCantBeNull;
        }
        
        return new Void();
    }

    public override IEnumerable<object> GetEqualityObjects()
    {
        yield return ViaEmail;
    }

    public static explicit operator string(GuestViaEmail guestViaEmail)
    {
        return guestViaEmail.ViaEmail;
    }
}
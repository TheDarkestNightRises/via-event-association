using System.Text.RegularExpressions;
using ViaEventAssociation.Core.Domain.Aggregates.Event.EventErrors;
using ViaEventAssociation.Core.Domain.Aggregates.Guest.GuestErrors;
using ViaEventAssociation.Core.Tools.OperationResult;
using Void = ViaEventAssociation.Core.Tools.OperationResult.Void;

namespace ViaEventAssociation.Core.Domain.Aggregates.Guest.Values;

public class GuestFirstName : ValueObject
{
    public string FirstName { get; }

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
        if (string.IsNullOrEmpty(firstName))
        {
            return GuestAggregateErrors.FirstName.FirstNameCantBeEmpty;
        }
        
        if (!Regex.IsMatch(firstName, @"^[a-zA-Z]+$"))
        {
            return GuestAggregateErrors.FirstName.FirstNameContainsInvalidCharacters;
        }

        if (firstName.Length >= 25)
        {
            return GuestAggregateErrors.FirstName.FirstNameTooLong;
        }

        if (firstName.Length <= 2)
        {
            return GuestAggregateErrors.FirstName.FirstNameTooShort;
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
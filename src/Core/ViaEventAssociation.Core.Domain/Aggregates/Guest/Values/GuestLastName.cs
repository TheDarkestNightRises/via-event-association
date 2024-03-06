using System.Text.RegularExpressions;
using ViaEventAssociation.Core.Domain.Aggregates.Event.EventErrors;
using ViaEventAssociation.Core.Domain.Aggregates.Guest.GuestErrors;
using ViaEventAssociation.Core.Tools.OperationResult;
using Void = ViaEventAssociation.Core.Tools.OperationResult.Void;

namespace ViaEventAssociation.Core.Domain.Aggregates.Guest.Values;

public partial class GuestLastName : ValueObject
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
        if (string.IsNullOrEmpty(lastName))
        {
            return GuestAggregateErrors.LastName.LastNameCantBeEmpty;
        }
        
        if (!AlphabeticCharacters().IsMatch(lastName))
        {
            return GuestAggregateErrors.LastName.LastNameContainsInvalidCharacters;
        }
        
        if (lastName.Length > 25)
        {
            return GuestAggregateErrors.LastName.LastNameTooLong;
        }

        if (lastName.Length < 2)
        {
            return GuestAggregateErrors.LastName.LastNameTooShort;
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

    [GeneratedRegex(@"^[a-zA-Z]+$")]
    private static partial Regex AlphabeticCharacters();
}
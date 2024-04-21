using System.Text.RegularExpressions;
using ViaEventAssociation.Core.Domain.Aggregates.Guest.GuestErrors;
using ViaEventAssociation.Core.Tools.OperationResult;
using Void = ViaEventAssociation.Core.Tools.OperationResult.Void;

namespace ViaEventAssociation.Core.Domain.Aggregates.Guest.Values;

public partial class GuestViaEmail : ValueObject
{
    public string ViaEmail { get; }

    internal GuestViaEmail(string viaEmail)
    {
        ViaEmail = viaEmail;
    }

    public static Result<GuestViaEmail> Create(string? email)
    {
        var validationResult = Validate(email);
        
        return validationResult.Match<Result<GuestViaEmail>>(
            onPayLoad: _ => new GuestViaEmail(email!),
            onError: errors => errors
        );
    }

    private static Result<Void> Validate(string? email)
    {
        var errors = new List<Error>();
        
        if (email is null)
        {
            return GuestAggregateErrors.ViaEmail.EmailCantBeEmpty;
        }

        if (!email.EndsWith("@via.dk"))
        {
            errors.Add(GuestAggregateErrors.ViaEmail.WrongDomain);
        }
        
        var username = email[..^"@via.dk".Length];
        
        if (username.Length is < 3 or > 6)
        {
            errors.Add(GuestAggregateErrors.ViaEmail.UsernameOutOfLength);
        }
        
        if (!ThreeOrFourLetters().IsMatch(username) && !SixDigits().IsMatch(username))
        {
            errors.Add(GuestAggregateErrors.ViaEmail.InvalidEmailFormat);
        }
        
        if (errors.Count != 0) return errors;
        
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

    [GeneratedRegex(@"^[a-zA-Z]{3,4}$")]
    private static partial Regex ThreeOrFourLetters();
    [GeneratedRegex(@"^\d{6}$")]
    private static partial Regex SixDigits();
}
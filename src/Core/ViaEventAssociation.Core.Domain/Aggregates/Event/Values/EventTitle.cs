
using System.Runtime.InteropServices.JavaScript;
using ViaEventAssociation.Core.Domain.Aggregates.Event.EventErrors;
using ViaEventAssociation.Core.Tools.OperationResult;
using Void = ViaEventAssociation.Core.Tools.OperationResult.Void;

namespace ViaEventAssociation.Core.Domain.Aggregates.Event.Values;

public class EventTitle : ValueObject
{
    internal string Title { get; }

    internal EventTitle(string title)
    {
        Title = title;
    }

    public static Result<EventTitle> Create(string? title)
    {
        var validationResult = Validate(title);

        return validationResult.Match<Result<EventTitle>>(
            onPayLoad: _ => new EventTitle(title!),
            onError: errors => errors); 

    }
    
    // validations
    public static Result<Void> Validate(string? newUpdatedTitle)
    {
        // non null input case 4
        if (newUpdatedTitle == null)
        {
            return EventAggregateErrors.TitleCanNotBeUpdatedWithNullValue;
        }        
        // length 3- 75 else error of legth case 1-2-3
        if (newUpdatedTitle.Length is < 3 or > 75)
        {
            return EventAggregateErrors.TitleUpdateInputNotValid;
        }
        return new Void();
    }

    public override IEnumerable<object> GetEqualityObjects()
    {
        yield return Title;
    }

    public static explicit operator string(EventTitle eventTitle)
    {
        return eventTitle.Title;
    }
    
}

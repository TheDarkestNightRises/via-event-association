
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Domain.Aggregates.Event.Values;

public class EventTitle : ValueObject
{
    protected string Title { get; }

    private EventTitle(string title)
    {
        Title = title;
    }

    public static Result<EventTitle> Create(string title)
    {
        return new EventTitle(title);
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

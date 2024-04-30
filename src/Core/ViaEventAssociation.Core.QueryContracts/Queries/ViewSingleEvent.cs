using ViaEventAssociation.Core.QueryContracts.Contract;

namespace ViaEventAssociation.Core.QueryContracts.Queries;

public abstract class ViewSingleEvent
{
    public class Query : IQuery<Answer> -- I broke stuff here for you
    {
        public string EventId { get; }

        private Query()
        {
        }

        public Query(string EventId)
        {
            this.EventId = EventId;
        }
    };
    public record Answer(ViewSingleEventInfo SingleEvent);
    public record ViewSingleEventInfo(
        string Title,
        string Description,
        string Start,
        string End,
        string Visibility,
        int NumberOfGuests
        );
    
}
using ViaEventAssociation.Core.QueryContracts.Contract;

namespace ViaEventAssociation.Core.QueryContracts.Queries;

public class UpcomingEventPage
{
    public record Query(int PageSize, int PageNumber, string? EventName) : IQuery<Answer>;

    public record Answer(List<EventOverview> EventOverviews);
    public record EventOverview(string Title, string Description, string Start, string End, string Visibility, int MaxCapacity, int NumberOfPartcicipants);
}
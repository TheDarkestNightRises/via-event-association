using Microsoft.EntityFrameworkCore;
using ViaEventAssociation.Core.QueryContracts.Contract;
using ViaEventAssociation.Core.QueryContracts.Queries;

namespace ViaEventAssociation.Infrastructure.EfcQueries.Queries;

public class UpcomingEventsPageQueryHandler(VeadatabaseProductionContext context): IQueryHandler<UpcomingEventPage.Query, UpcomingEventPage.Answer>
{
    public async Task<UpcomingEventPage.Answer> HandleAsync(UpcomingEventPage.Query query)
    {
        var events = await context.Events
            .OrderBy(e => e.Start)
            .Skip((query.PageSize * (query.PageNumber - 1)))
            .Take(query.PageSize)
            .Where(e => e.Title == query.EventName)
            .Where(e => e.Status == "Active")
            .Select(e => new UpcomingEventPage.EventOverview(e.Title, e.Description, e.Start, e.End, e.Visibility,
                e.Capacity, 6))
            .ToListAsync();
        
        return new UpcomingEventPage.Answer(events);
    }
}

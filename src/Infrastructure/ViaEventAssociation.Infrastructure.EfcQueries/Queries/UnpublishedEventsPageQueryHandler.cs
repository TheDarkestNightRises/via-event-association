using Microsoft.EntityFrameworkCore;
using ViaEventAssociation.Core.QueryContracts.Contract;
using ViaEventAssociation.Core.QueryContracts.Queries;
using ViaEventAssociation.Infrastructure.EfcQueries.Context;

namespace ViaEventAssociation.Infrastructure.EfcQueries.Queries;

public class UnpublishedEventsPageQueryHandler(VeadatabaseProductionContext context): IQueryHandler<UnpublishedEvents.Query, UnpublishedEvents.Answer>
{
    public async Task<UnpublishedEvents.Answer> HandleAsync(UnpublishedEvents.Query query)
    {
        var canceledEvents = await context.Events
            .Where(e => e.Status == "Canceled")
            .Select(e => new UnpublishedEvents.EventDetails(e.Title))
            .ToListAsync();

        var draftEvents = await context.Events
            .Where(e => e.Status == "Draft")
            .Select(e => new UnpublishedEvents.EventDetails(e.Title))
            .ToListAsync();

        var readyEvents = await context.Events
            .Where(e => e.Status == "Ready")
            .Select(e => new UnpublishedEvents.EventDetails(e.Title))
            .ToListAsync();
        
        return new UnpublishedEvents.Answer(canceledEvents, draftEvents, readyEvents);
    }
}

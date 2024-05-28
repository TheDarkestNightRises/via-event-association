using Microsoft.EntityFrameworkCore;
using ViaEventAssociation.Core.QueryContracts.Contract;
using ViaEventAssociation.Core.QueryContracts.Queries;
using ViaEventAssociation.Infrastructure.EfcQueries.Context;

namespace ViaEventAssociation.Infrastructure.EfcQueries.Queries;

public class ViewSingleEventQueryHandler(VeadatabaseProductionContext context) : IQueryHandler<ViewSingleEvent.Query, ViewSingleEvent.Answer>
{
    public async Task<ViewSingleEvent.Answer> HandleAsync(ViewSingleEvent.Query query)
    {
        var numberOfGuests = await context.Invitations
            .CountAsync(inv => inv.EventId == query.Id && inv.Status == "Accepted");
        
        var singleEvent = await context.Events
            .Where(e => e.Id == query.Id)
            .Select(e => new ViewSingleEvent.ViewSingleEventInfo(e.Title, e.Description, e.Start, e.End, e.Visibility, numberOfGuests))
            .FirstOrDefaultAsync();
        
        return new ViewSingleEvent.Answer(singleEvent!);
    }
}
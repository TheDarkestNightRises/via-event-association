﻿using Microsoft.EntityFrameworkCore;
using ViaEventAssociation.Core.QueryContracts.Contract;
using ViaEventAssociation.Core.QueryContracts.Queries;

namespace ViaEventAssociation.Infrastructure.EfcQueries.Queries;

public class ViewSingleEventQueryHandler(VeadatabaseProductionContext context) : IQueryHandler<ViewSingleEvent.Query, ViewSingleEvent.Answer>
{
    public async Task<ViewSingleEvent.Answer> HandleAsync(ViewSingleEvent.Query query)
    {
        var numberOfGuests = await context.Invitations
            .CountAsync(inv => inv.EventId == query.EventId && inv.Status == "Accepted");
        
        var singleEvent = await context.Events
            .Where(e => e.Id == query.EventId)
            .Select(e => new ViewSingleEvent.ViewSingleEventInfo(e.Title, e.Description, e.Start, e.End, e.Visibility, numberOfGuests))
            .FirstOrDefaultAsync();
        
        return new ViewSingleEvent.Answer(singleEvent!);
    }
}
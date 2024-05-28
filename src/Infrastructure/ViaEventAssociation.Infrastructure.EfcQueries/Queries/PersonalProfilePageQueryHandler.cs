using Microsoft.EntityFrameworkCore;
using ViaEventAssociation.Core.QueryContracts.Contract;
using ViaEventAssociation.Core.QueryContracts.Queries;
using ViaEventAssociation.Infrastructure.EfcQueries.Context;

namespace ViaEventAssociation.Infrastructure.EfcQueries.Queries;

public class PersonalProfilePageQueryHandler(VeadatabaseProductionContext context): IQueryHandler<PersonalProfilePage.Query, PersonalProfilePage.Answer>
{
    public async Task<PersonalProfilePage.Answer> HandleAsync(PersonalProfilePage.Query query)
    {
        var timeProvider = query.TimeProvider ?? TimeProvider.System;
        string currentTime =timeProvider.GetLocalNow().ToString("yyyy-MM-ddTHH:mm:ss");
        var guestPage = await context.Guests
            .Where(guest => guest.Id == query.GuestId)
            .Select(guest => new
            {
                FullName = guest.FirstName + guest.LastName,
                guest.Email,
                guest.PictureUrl
            }).FirstAsync();
      
        var upcomingEvents = await context.Invitations
            .Where(invitation => invitation.GuestId ==  query.GuestId && invitation.Status == "Accepted" && invitation.Event.Start.CompareTo(currentTime) < 0)
            .OrderBy(invitation => invitation.Event!.Start)
            .Select(invitation => new PersonalProfilePage.UpcomingEvents(
                 invitation.Event!.Title,
                 invitation.Event.Invitations.Count(inv => inv.Status == "Accepted"),
                 invitation.Event.Start!
                )
            ).ToListAsync();
        var pastEvents = await context.Invitations
            .Where(invitation => invitation.GuestId ==  query.GuestId && invitation.Status == "Accepted" && invitation.Event.Start.CompareTo(currentTime) > 0)
            .OrderByDescending(invitation => invitation.Event!.Start)
            .Select(invitation => new PersonalProfilePage.PastEvents(invitation.Event!.Title)
           ).Take(5).ToListAsync();
        var pendingInvitationsCount = await context.Invitations
            .CountAsync(invitation => invitation.GuestId == query.GuestId && invitation.Status == "Pending");

        var personalProfilePage = new PersonalProfilePage.PersonalProfile(guestPage.FullName, guestPage.Email,
            guestPage.PictureUrl, upcomingEvents.Count, upcomingEvents, pastEvents, pendingInvitationsCount);
        
        return new PersonalProfilePage.Answer(personalProfilePage);
    }
}
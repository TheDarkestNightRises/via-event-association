using ViaEventAssociation.Infrastructure.EfcDmPersistence.Context;

namespace ViaEventAssociation.Infrastructure.EfcDmPersistence.SeedFactory;

public static class Seeder
{
    public static DmContext Seed(DmContext context)
    {
        // context.Guests.AddRange(GuestSeedFactory.CreateGuests());
        // context.Events.AddRange(EventSeedFactory.CreateEvents());
        /*context.Invitations.AddRange(InvitationSeedFactory.CreateInvitations());*/
        context.SaveChanges();
        return context;
    }
}
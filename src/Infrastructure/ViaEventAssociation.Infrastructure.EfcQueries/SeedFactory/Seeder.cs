namespace ViaEventAssociation.Infrastructure.EfcQueries.SeedFactory;

public static class Seeder
{
    public static VeadatabaseProductionContext Seed(this VeadatabaseProductionContext context)
    {
        context.Guests.AddRange(GuestSeedFactory.CreateGuests());
        context.Events.AddRange(EventSeedFactory.CreateEvents());
        /*context.Invitations.AddRange(InvitationSeedFactory.CreateInvitations());*/
        context.SaveChanges();
        return context;
    }
}
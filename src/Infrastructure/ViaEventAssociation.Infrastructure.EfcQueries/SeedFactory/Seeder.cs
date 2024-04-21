namespace ViaEventAssociation.Infrastructure.EfcQueries.SeedFactory;

public static class Seeder
{
    public static async Task<VeadatabaseProductionContext> Seed(this VeadatabaseProductionContext context)
    {
        await context.Guests.AddRangeAsync(GuestSeedFactory.CreateGuests());
        await context.Events.AddRangeAsync(EventSeedFactory.CreateEvents());
        await context.Invitations.AddRangeAsync(InvitationSeedFactory.CreateInvitations());
        await context.SaveChangesAsync();
        return context;
    }
}
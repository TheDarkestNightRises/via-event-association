using ViaEventAssociation.Core.Domain.Aggregates.Guest.Values;

namespace ViaEventAssociation.Core.Domain.Aggregates.Guest.Repository;

public interface IGuestRepository
{
    Task<GuestAggregate> GetAsync(GuestId commandGuestId);
}
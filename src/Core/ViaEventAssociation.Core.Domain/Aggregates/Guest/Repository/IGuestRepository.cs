using ViaEventAssociation.Core.Domain.Aggregates.Guest.Values;
using ViaEventAssociation.Core.Domain.Common.Repository;

namespace ViaEventAssociation.Core.Domain.Aggregates.Guest.Repository;

public interface IGuestRepository: IGenericRepository<GuestAggregate,GuestId>
{
    
}
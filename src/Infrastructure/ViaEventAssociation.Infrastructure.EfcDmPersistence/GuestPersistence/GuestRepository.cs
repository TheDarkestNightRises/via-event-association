using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyModel.Resolution;
using ViaEventAssociation.Core.Domain.Aggregates.Guest;
using ViaEventAssociation.Core.Domain.Aggregates.Guest.Repository;
using ViaEventAssociation.Core.Domain.Aggregates.Guest.Values;
using ViaEventAssociation.Infrastructure.EfcDmPersistence.Repository;

namespace ViaEventAssociation.Infrastructure.EfcDmPersistence.GuestPersistence;

public class GuestRepository(DbContext context) : RepositoryBase<GuestAggregate, GuestId>(context), IGuestRepository
{
    
}
using ViaEventAssociation.Core.Domain.Common.UnitOfWork;
using ViaEventAssociation.Infrastructure.EfcDmPersistence.Context;

namespace ViaEventAssociation.Infrastructure.EfcDmPersistence.UnitOfWork;

public class SqliteUnitOfWork(DmContext context) : IUnitOfWork
{
    public Task SaveChangesAsync() => context.SaveChangesAsync();
}
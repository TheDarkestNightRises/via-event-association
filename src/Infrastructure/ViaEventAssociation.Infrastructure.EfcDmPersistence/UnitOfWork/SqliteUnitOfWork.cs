using Microsoft.EntityFrameworkCore;
using ViaEventAssociation.Core.Domain.Common.UnitOfWork;

namespace ViaEventAssociation.Infrastructure.EfcDmPersistence.UnitOfWork;

public class SqliteUnitOfWork(DbContext context) : IUnitOfWork
{
    public Task SaveChangesAsync() => context.SaveChangesAsync();
}
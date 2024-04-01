namespace ViaEventAssociation.Core.Domain.Common.UnitOfWork;

public interface IUnitOfWork
{
     public Task SaveChangesAsync();
}
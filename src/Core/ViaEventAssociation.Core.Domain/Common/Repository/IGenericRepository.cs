namespace ViaEventAssociation.Core.Domain.Common.Repository;

public interface IGenericRepository<T, TId> where T: AggregateRoot<TId> //TODO: add constraints
{
    Task<T> GetAsync(TId id);
    Task RemoveAsync(TId id);
    Task AddAsync(T aggregate);
}
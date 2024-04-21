using Microsoft.EntityFrameworkCore;
using ViaEventAssociation.Core.Domain;
using ViaEventAssociation.Core.Domain.Common.Repository;

namespace ViaEventAssociation.Infrastructure.EfcDmPersistence.Repository;

public abstract class RepositoryBase<TAgg,TId>(DbContext context): IGenericRepository<TAgg,TId> where TAgg : AggregateRoot<TId>
{
    public virtual async Task<TAgg> GetAsync(TId id)
    {
        _ = id ?? throw new ArgumentNullException(nameof(id), "Id cannot be null.");
        TAgg? root = await context.Set<TAgg>().FindAsync(id);
        return root!;
    }

    public virtual async Task RemoveAsync(TId id)
    {
        _ = id ?? throw new ArgumentNullException(nameof(id), "Id cannot be null.");
        TAgg? root = await context.Set<TAgg>().FindAsync(id);
        context.Set<TAgg>().Remove(root!);
    }

    public virtual async Task AddAsync(TAgg aggregate)
    {
        await context.Set<TAgg>().AddAsync(aggregate);
    }
}
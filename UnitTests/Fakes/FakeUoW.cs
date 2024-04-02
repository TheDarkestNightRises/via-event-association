using ViaEventAssociation.Core.Domain.Common.UnitOfWork;

namespace UnitTests.Fakes;

public class FakeUoW : IUnitOfWork
{
    public Task SaveChangesAsync()
    {
        return Task.CompletedTask;
    }
}
using ViaEventAssociation.Core.Application.CommandDispatching.Commands;
using ViaEventAssociation.Core.Application.CommandDispatching.Commands.Event;
using ViaEventAssociation.Core.Domain.Aggregates.Event;
using ViaEventAssociation.Core.Domain.Aggregates.Event.Repository;
using ViaEventAssociation.Core.Domain.Common.UnitOfWork;
using ViaEventAssociation.Core.Tools.OperationResult;
using Void = ViaEventAssociation.Core.Tools.OperationResult.Void;

namespace ViaEventAssociation.Core.Application.Features.Event;

public class CreateNewEventCommandHandler(IEventRepository repository, IUnitOfWork uow) : ICommandHandler<CreateNewEventCommand>
{
    public async Task<Result<Void>> HandleAsync(CreateNewEventCommand command)
    {
        var createdEvent = EventAggregate.Create();
        await repository.AddAsync(createdEvent.PayLoad);
        await uow.SaveChangesAsync();

        return new Void();
    }
}
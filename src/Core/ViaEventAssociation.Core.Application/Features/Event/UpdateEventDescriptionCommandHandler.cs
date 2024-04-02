using ViaEventAssociation.Core.Application.CommandDispatching.Commands;
using ViaEventAssociation.Core.Application.CommandDispatching.Commands.Event;
using ViaEventAssociation.Core.Domain.Aggregates.Event.Repository;
using ViaEventAssociation.Core.Domain.Common.UnitOfWork;
using ViaEventAssociation.Core.Tools.OperationResult;
using Void = ViaEventAssociation.Core.Tools.OperationResult.Void;

namespace ViaEventAssociation.Core.Application.Features.Event;

public class UpdateEventDescriptionCommandHandler(IEventRepository repository, IUnitOfWork uow) : ICommandHandler<UpdateEventDescriptionCommand>
{
    public async Task<Result<Void>> HandleAsync(UpdateEventDescriptionCommand command)
    {
        var evt = await repository.GetAsync(command.Id);
        var result = evt.UpdateEventDescription(command.Description);

        if (result.IsFailure)
        {
            return result.Errors;
        }

        await uow.SaveChangesAsync();
        return new Void();
    }
}
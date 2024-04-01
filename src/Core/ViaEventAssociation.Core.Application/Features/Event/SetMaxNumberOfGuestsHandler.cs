using ViaEventAssociation.Core.Application.CommandDispatching.Commands;
using ViaEventAssociation.Core.Application.CommandDispatching.Commands.Event;
using ViaEventAssociation.Core.Domain.Aggregates.Event;
using ViaEventAssociation.Core.Domain.Aggregates.Event.Repository;
using ViaEventAssociation.Core.Domain.Common.UnitOfWork;
using ViaEventAssociation.Core.Tools.OperationResult;
using Void = ViaEventAssociation.Core.Tools.OperationResult.Void;

namespace ViaEventAssociation.Core.Application.Features.Event;

public class SetMaxNumberOfGuestsHandler : ICommandHandler<SetMaxNumberOfGuestsCommand>
{
    private readonly IEventRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public SetMaxNumberOfGuestsHandler(IEventRepository repository, IUnitOfWork unitOfWork) =>
        (_repository, _unitOfWork) = (repository, unitOfWork);
    
    public async Task<Result<Void>> HandleAsync(SetMaxNumberOfGuestsCommand command) //TODO: add time provider when fixed
    {
        EventAggregate evt = await _repository.GetAsync(command.Id);
        Result<Void> result = evt.SetNumberOfGuests(command.EventCapacity);
        if (result.IsFailure)
            return result;
        await _unitOfWork.SaveChangesAsync();
        return new Void();
    }
}
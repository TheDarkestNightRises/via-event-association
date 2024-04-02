using ViaEventAssociation.Core.Domain.Aggregates.Event.Values;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Application.CommandDispatching.Commands.Event;

public class SetMaxNumberOfGuestsCommand
{
    public EventId Id { get; }
    public EventCapacity EventCapacity { get; }
    private SetMaxNumberOfGuestsCommand(EventId id, EventCapacity capacity) =>
        (Id, EventCapacity) = (id, capacity);

    public static Result<SetMaxNumberOfGuestsCommand> Create(string id,int capacity)
    {
        Result<EventId> idResult = EventId.FromString(id);
        if (idResult.IsFailure)
            return idResult.Errors;
        
        Result<EventCapacity> result = EventCapacity.Create(capacity);
        if (result.IsFailure)
            return result.Errors;
        
        return new SetMaxNumberOfGuestsCommand(idResult.PayLoad, result.PayLoad);
    }
}
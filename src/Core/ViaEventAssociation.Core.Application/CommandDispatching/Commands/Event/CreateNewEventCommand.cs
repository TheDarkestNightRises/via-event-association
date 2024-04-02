using ViaEventAssociation.Core.Domain.Aggregates.Event.Values;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Application.CommandDispatching.Commands.Event;

public class CreateNewEventCommand
{
    public EventId Id { get; }

    private CreateNewEventCommand(EventId id)
    {
        Id = id;
    }
    
    public static Result<CreateNewEventCommand> Create()
    {
        return new CreateNewEventCommand(EventId.Create());
    }
}


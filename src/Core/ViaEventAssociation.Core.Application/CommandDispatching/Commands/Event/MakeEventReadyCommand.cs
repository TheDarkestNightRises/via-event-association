using ViaEventAssociation.Core.Domain.Aggregates.Event.Values;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Application.CommandDispatching.Commands.Event;

public class MakeEventReadyCommand
{
    public EventId Id { get; }

    public MakeEventReadyCommand(EventId id)
    {
        Id = id;
    }
    
    public static Result<MakeEventReadyCommand> Create(string id)
    {
        var idResult = EventId.FromString(id);

        if (idResult.IsFailure)
        {
            return idResult.Errors;
        }
        
        return new MakeEventReadyCommand(idResult.PayLoad);
    }
}
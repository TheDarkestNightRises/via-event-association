using ViaEventAssociation.Core.Domain.Aggregates.Event.Values;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Application.CommandDispatching.Commands.Event;

public class CreatorActivatesAnEventCommand
{
    public EventId Id { get; }
    
    private CreatorActivatesAnEventCommand(EventId id)
    {
        Id = id;
    }
    
    public static Result<CreatorActivatesAnEventCommand> Create(string id)
    {
        var idResult = EventId.FromString(id);

        if (idResult.IsFailure)
        {
            return idResult.Errors;
        }

        return new CreatorActivatesAnEventCommand(idResult.PayLoad);
    }
    
}
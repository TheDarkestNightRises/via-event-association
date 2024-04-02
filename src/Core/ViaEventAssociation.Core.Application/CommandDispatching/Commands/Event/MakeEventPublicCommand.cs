using ViaEventAssociation.Core.Domain.Aggregates.Event.Values;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Application.CommandDispatching.Commands.Event;

public class MakeEventPublicCommand
{
    public EventId Id { get; }
    private MakeEventPublicCommand(EventId id)
    {
        Id = id;
    }
    public static Result<MakeEventPublicCommand> Create(string id)
    {
        var idResult = EventId.FromString(id);

        if (idResult.IsFailure)
        {
            return idResult.Errors;
        }
        
        return new MakeEventPublicCommand(idResult.PayLoad);
    }
}
using ViaEventAssociation.Core.Domain.Aggregates.Event.Values;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Application.CommandDispatching.Commands.Event;

public class MakeEventPrivateCommand
{
    public EventId Id { get; }
    private MakeEventPrivateCommand(EventId id)
    {
        Id = id;
    }
    public static Result<MakeEventPrivateCommand> Create(string id)
    {
        var idResult = EventId.FromString(id);

        if (idResult.IsFailure)
        {
            return idResult.Errors;
        }
        
        return new MakeEventPrivateCommand(idResult.PayLoad);
    }
    
}
using ViaEventAssociation.Core.Domain.Aggregates.Event.Values;
using ViaEventAssociation.Core.Tools.OperationResult;
using Void = ViaEventAssociation.Core.Tools.OperationResult.Void;

namespace ViaEventAssociation.Core.Application.CommandDispatching.Commands.Event;

public class UpdateEventDescriptionCommand
{
    public EventId Id { get; }
    public EventDescription Description { get; }
    
    private UpdateEventDescriptionCommand(EventId id, EventDescription description)
    {
        Id = id;
        Description = description;
    }
    
    public static Result<UpdateEventDescriptionCommand> Create(string id, string description)
    {
        var idResult = EventId.FromString(id);
        var descriptionResult = EventDescription.Create(description);

        if (idResult.IsFailure)
        {
            return idResult.Errors;
        }

        if (descriptionResult.IsFailure)
        {
            return descriptionResult.Errors;
        }

        return new UpdateEventDescriptionCommand(idResult.PayLoad, descriptionResult.PayLoad);
    }
}
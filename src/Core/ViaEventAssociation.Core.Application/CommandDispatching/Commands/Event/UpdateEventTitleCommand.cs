using ViaEventAssociation.Core.Domain.Aggregates.Event.Repository;
using ViaEventAssociation.Core.Domain.Aggregates.Event.Values;
using ViaEventAssociation.Core.Domain.Common.UnitOfWork;
using ViaEventAssociation.Core.Tools.OperationResult;
using Void = ViaEventAssociation.Core.Tools.OperationResult.Void;

namespace ViaEventAssociation.Core.Application.CommandDispatching.Commands.Event;

public class UpdateEventTitleCommand
{
    public EventId Id { get; }
    public EventTitle Title { get; }
    
    private UpdateEventTitleCommand(EventId id, EventTitle title)
    {
        Id = id;
        Title = title;
    }

    public static Result<UpdateEventTitleCommand> Create(string id, string title)
    {
        var idResult = EventId.FromString(id);
        var titleResult = EventTitle.Create(title);

        if (idResult.IsFailure)
        {
            return idResult.Errors;
        }

        if (titleResult.IsFailure)
        {
            return titleResult.Errors;
        }

        return new UpdateEventTitleCommand(idResult.PayLoad, titleResult.PayLoad);
    }
}
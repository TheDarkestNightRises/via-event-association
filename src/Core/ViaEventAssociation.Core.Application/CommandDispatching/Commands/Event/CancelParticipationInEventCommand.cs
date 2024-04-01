using ViaEventAssociation.Core.Domain.Aggregates.Event.Values;
using ViaEventAssociation.Core.Domain.Aggregates.Guest.Values;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Application.CommandDispatching.Commands.Event;

public class CancelParticipationInEventCommand
{
    public EventId Id { get; }
    public GuestId GuestId { get; }
    private CancelParticipationInEventCommand(EventId id, GuestId interval) =>
        (Id, GuestId) = (id, interval);

    public static Result<CancelParticipationInEventCommand> Create(string id,string guestId)
    {
        Result<EventId> idResult = EventId.FromString(id);
        if (idResult.IsFailure)
            return idResult.Errors;
        
        Result<GuestId> result = GuestId.FromString(guestId);
        if (result.IsFailure)
            return result.Errors;
        
        return new CancelParticipationInEventCommand(idResult.PayLoad, result.PayLoad);
    }
}
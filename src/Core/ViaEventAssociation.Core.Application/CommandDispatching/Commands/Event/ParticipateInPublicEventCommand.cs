using ViaEventAssociation.Core.Domain.Aggregates.Event.Values;
using ViaEventAssociation.Core.Domain.Aggregates.Guest.Values;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Application.CommandDispatching.Commands.Event;

public class ParticipateInPublicEventCommand
{
    public EventId Id { get; }
    public GuestId GuestId { get; }
    private ParticipateInPublicEventCommand(EventId id, GuestId interval) =>
        (Id, GuestId) = (id, interval);

    public static Result<ParticipateInPublicEventCommand> Create(string id,string guestId)
    {
        var idResult = EventId.FromString(id);
        if (idResult.IsFailure)
            return idResult.Errors;
        
        var result = GuestId.FromString(guestId);
        if (result.IsFailure)
            return result.Errors;
        
        return new ParticipateInPublicEventCommand(idResult.PayLoad, result.PayLoad);
    }
}
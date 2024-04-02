using ViaEventAssociation.Core.Domain.Aggregates.Event.Values;
using ViaEventAssociation.Core.Domain.Aggregates.Guest.Values;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Application.CommandDispatching.Commands.Event;

public class GuestIsInvitedToEventCommand
{
    public EventId EventId { get; }
    public GuestId GuestId { get; }
    
    private GuestIsInvitedToEventCommand(EventId eventId, GuestId guestId)
    {
        EventId = eventId;
        GuestId = guestId;
    }
    
    public static Result<GuestIsInvitedToEventCommand> Create(string eventId, string guestId)
    {
        var eventIdResult = EventId.FromString(eventId);
        var guestIdResult = GuestId.FromString(guestId);

        if (eventIdResult.IsFailure)
        {
            return eventIdResult.Errors;
        }

        if (guestIdResult.IsFailure)
        {
            return guestIdResult.Errors;
        }

        return new GuestIsInvitedToEventCommand(eventIdResult.PayLoad, guestIdResult.PayLoad);
    }
}
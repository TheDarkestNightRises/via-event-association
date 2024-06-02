using ViaEventAssociation.Core.Domain.Aggregates.Event.Values;
using ViaEventAssociation.Core.Domain.Aggregates.Guest.Values;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Application.CommandDispatching.Commands.Guest;

public class GuestDeclinesInvitationCommand(EventId eventId, GuestId guestId)
{
    public EventId EventId { get; } = eventId;
    public GuestId GuestId { get; } = guestId;

    public static Result<GuestDeclinesInvitationCommand> Create(string eventId, string guestId)
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

        return new GuestDeclinesInvitationCommand(eventIdResult.PayLoad, guestIdResult.PayLoad);
    }
}
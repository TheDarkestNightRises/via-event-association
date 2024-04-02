using ViaEventAssociation.Core.Application.CommandDispatching.Commands;
using ViaEventAssociation.Core.Application.CommandDispatching.Commands.Event;
using ViaEventAssociation.Core.Domain.Aggregates.Event;
using ViaEventAssociation.Core.Domain.Aggregates.Event.Repository;
using ViaEventAssociation.Core.Domain.Aggregates.Guest.Repository;
using ViaEventAssociation.Core.Domain.Aggregates.Guest.Values;
using ViaEventAssociation.Core.Domain.Common.UnitOfWork;
using ViaEventAssociation.Core.Tools.OperationResult;
using Void = ViaEventAssociation.Core.Tools.OperationResult.Void;

namespace ViaEventAssociation.Core.Application.Features.Event;

public class GuestIsInvitedToEventCommandHandler(
    IEventRepository repository,
    IGuestRepository guestRepository,
    IUnitOfWork uow) : ICommandHandler<GuestIsInvitedToEventCommand>
{
    public async Task<Result<Void>> HandleAsync(GuestIsInvitedToEventCommand command)
    {
        var @event = await repository.GetAsync(command.EventId);
        var guest = await guestRepository.GetAsync(command.GuestId);
        var result = @event.InviteGuestToEvent(guest.Id);
        if(result.IsFailure)
        {
            return result.Errors;
        }
        await uow.SaveChangesAsync();
        return new Void();
    }
}
using ViaEventAssociation.Core.Application.CommandDispatching.Commands;
using ViaEventAssociation.Core.Application.CommandDispatching.Commands.Guest;
using ViaEventAssociation.Core.Domain.Aggregates.Event.Repository;
using ViaEventAssociation.Core.Domain.Aggregates.Guest.Repository;
using ViaEventAssociation.Core.Domain.Common.UnitOfWork;
using ViaEventAssociation.Core.Tools.OperationResult;
using Void = ViaEventAssociation.Core.Tools.OperationResult.Void;

namespace ViaEventAssociation.Core.Application.Features.Event;

public class GuestDeclinesInvitationCommandHandler(IEventRepository eventRepository,IGuestRepository guestRepository, IUnitOfWork uow) : ICommandHandler<GuestDeclinesInvitationCommand>
{
    public async Task<Result<Void>> HandleAsync(GuestDeclinesInvitationCommand command)
    {
        var evt = await eventRepository.GetAsync(command.EventId);
        var guest = await guestRepository.GetAsync(command.GuestId);

        var result = evt.GuestDeclinesInvitation(guest.Id);
        
        if (result.IsFailure)
        {
            return result;
        }
        
        await uow.SaveChangesAsync();
        return new Void();
    }
}
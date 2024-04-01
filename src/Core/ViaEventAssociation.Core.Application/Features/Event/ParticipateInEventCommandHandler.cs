﻿using ViaEventAssociation.Core.Application.CommandDispatching.Commands;
using ViaEventAssociation.Core.Application.CommandDispatching.Commands.Event;
using ViaEventAssociation.Core.Domain.Aggregates.Event.Repository;
using ViaEventAssociation.Core.Domain.Common.UnitOfWork;
using ViaEventAssociation.Core.Tools.OperationResult;
using Void = ViaEventAssociation.Core.Tools.OperationResult.Void;

namespace ViaEventAssociation.Core.Application.Features.Event;

public class ParticipateInEventCommandHandler(IEventRepository repository, IUnitOfWork uow) : ICommandHandler<ParticipateInPublicEventCommand>
{
    public async Task<Result<Void>> HandleAsync(ParticipateInPublicEventCommand command)
    {
        var evt = await repository.GetAsync(command.Id);
        var result = evt.ParticipateInPublicEvent(command.GuestId);

        if (result.IsFailure)
        {
            return result;
        }

        await uow.SaveChangesAsync();
        return new Void();
    }
}
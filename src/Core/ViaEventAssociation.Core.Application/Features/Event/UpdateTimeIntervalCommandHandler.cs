﻿using ViaEventAssociation.Core.Application.CommandDispatching.Commands;
using ViaEventAssociation.Core.Application.CommandDispatching.Commands.Event;
using ViaEventAssociation.Core.Domain.Aggregates.Event;
using ViaEventAssociation.Core.Domain.Aggregates.Event.Repository;
using ViaEventAssociation.Core.Domain.Common.UnitOfWork;
using ViaEventAssociation.Core.Tools.OperationResult;
using Void = ViaEventAssociation.Core.Tools.OperationResult.Void;

namespace ViaEventAssociation.Core.Application.Features.Event;

public class UpdateTimeIntervalCommandHandler(IEventRepository repository, IUnitOfWork unitOfWork, TimeProvider timeProvider)
    : ICommandHandler<UpdateTimeIntervalCommand>
{
    public async Task<Result<Void>> HandleAsync(UpdateTimeIntervalCommand command)
    {
        Console.WriteLine(command.Id);
        EventAggregate evt = await repository.GetAsync(command.Id);
        Result<Void> result = evt.UpdateEventTimeInterval(command.EventTimeInterval, timeProvider);
        if (result.IsFailure)
            return result;
        await unitOfWork.SaveChangesAsync();
        return new Void();
    }
}
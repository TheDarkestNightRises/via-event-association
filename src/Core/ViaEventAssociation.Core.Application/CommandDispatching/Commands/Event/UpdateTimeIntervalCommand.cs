using ViaEventAssociation.Core.Domain.Aggregates.Event.Values;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Application.CommandDispatching.Commands.Event;

public class UpdateTimeIntervalCommand
{
    public EventId Id { get; }
    public EventTimeInterval EventTimeInterval { get; }
    private UpdateTimeIntervalCommand(EventId id, EventTimeInterval interval) =>
        (Id, EventTimeInterval) = (id, interval);

    public static Result<UpdateTimeIntervalCommand> Create(string id, DateTime start, DateTime end,
        TimeProvider? timeProvider = null)
    {
        Result<EventId> idResult = EventId.FromString(id);
        if (idResult.IsFailure)
            return idResult.Errors;
        
        Result<EventTimeInterval> timeIntervalResult = EventTimeInterval.Create(start, end, timeProvider); //TODO: remove time provider when implementation fixed
        if (timeIntervalResult.IsFailure)
            return timeIntervalResult.Errors;
        
        return new UpdateTimeIntervalCommand(idResult.PayLoad, timeIntervalResult.PayLoad);
    }
}

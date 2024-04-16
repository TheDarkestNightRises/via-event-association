using ViaEventAssociation.Core.Domain.Aggregates.Event.EventErrors;
using ViaEventAssociation.Core.Tools.OperationResult;
using Void = ViaEventAssociation.Core.Tools.OperationResult.Void;

namespace ViaEventAssociation.Core.Domain.Aggregates.Event.Values;

public class EventTimeInterval : ValueObject
{
    public DateTime Start { get; }
    public DateTime End { get; }
    internal TimeProvider CurrentTimeProvider;

    // internal EventTimeInterval(DateTime start, DateTime end)
    // {
    //     Start = start;
    //     End = end;
    //     CurrentTimeProvider = TimeProvider.System;
    // }
    
    internal EventTimeInterval(DateTime start, DateTime end, TimeProvider? provider = null)
    {
        Start = start;
        End = end;
        CurrentTimeProvider = provider ?? TimeProvider.System;
    }
    
    private EventTimeInterval(DateTime start, DateTime end)
    {
        Start = start;
        End = end;
        CurrentTimeProvider = TimeProvider.System;
    }


    public static Result<EventTimeInterval> Create(DateTime start, DateTime end, TimeProvider? currentTimeProvider = null)
    {
        var validationResult = Validate(start, end, currentTimeProvider);
        return validationResult.Match<Result<EventTimeInterval>>(
            onPayLoad: _ => new EventTimeInterval(start, end, currentTimeProvider),
            onError: errors => errors); 

    }
    
    // validations
    public static Result<Void> Validate(DateTime start, DateTime end, TimeProvider? currentTimeProvider = null)
    {
        currentTimeProvider ??= TimeProvider.System;
        if (start > end)
            return EventAggregateErrors.EndTimeBeforeStartTime;
        if(end - start < new TimeSpan(1, 0, 0))
            return EventAggregateErrors.EventDurationOufOfRange;
        if(end - start > new TimeSpan(10, 0, 0))
            return EventAggregateErrors.EventDurationOufOfRange;
        if(start <= currentTimeProvider.GetLocalNow())
            return EventAggregateErrors.EventInThePast;
        if(TimeOnly.FromDateTime(start) < new TimeOnly(8, 0))
            return EventAggregateErrors.TimeIntervalUnavailable;
        if(TimeOnly.FromDateTime(end) < new TimeOnly(8, 0) && TimeOnly.FromDateTime(end) > new TimeOnly(1, 0) )
            return EventAggregateErrors.TimeIntervalUnavailable;
        if(TimeOnly.FromDateTime(end) >= new TimeOnly(8, 0) && DateOnly.FromDateTime(start) < DateOnly.FromDateTime(end))
            return EventAggregateErrors.TimeIntervalUnavailable;
        return new Void();
    }
    
    public static Result<Void> Validate(EventTimeInterval interval)
    {
        
        return Validate(interval.Start, interval.End, interval.CurrentTimeProvider);
    }
    

    public override IEnumerable<object> GetEqualityObjects()
    {
        yield return Start;
        yield return End;
    }

    // public static explicit operator string(EventTitle eventTitle)
    // {
    //     return eventTitle.Title;
    // }
}
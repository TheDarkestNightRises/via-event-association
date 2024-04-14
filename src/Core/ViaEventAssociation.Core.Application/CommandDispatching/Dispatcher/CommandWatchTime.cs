using System.Diagnostics;
using ViaEventAssociation.Core.Tools.OperationResult;
using Void = ViaEventAssociation.Core.Tools.OperationResult.Void;

namespace ViaEventAssociation.Core.Application.CommandDispatching.Dispatcher;

public class CommandWatchTime(ICommandDispatcher dispatcher) : ICommandDispatcher
{
    public TimeSpan ElapsedTime;
    public async Task<Result<Void>> DispatchAsync<TCommand>(TCommand command)
    {
        var stopwatch = new Stopwatch();
        stopwatch.Start();

        var result = await dispatcher.DispatchAsync(command);

        ElapsedTime = stopwatch.Elapsed;
        Console.WriteLine($"Command was finished in {ElapsedTime}");
        return result;
    }
}
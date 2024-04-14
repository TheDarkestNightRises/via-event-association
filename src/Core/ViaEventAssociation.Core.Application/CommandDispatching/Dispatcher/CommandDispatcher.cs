using ViaEventAssociation.Core.Application.CommandDispatching.Commands;
using ViaEventAssociation.Core.Tools.OperationResult;
using Void = ViaEventAssociation.Core.Tools.OperationResult.Void;
using Microsoft.Extensions.DependencyInjection;

namespace ViaEventAssociation.Core.Application.CommandDispatching.Dispatcher;

public class CommandDispatcher(IServiceProvider serviceProvider) : ICommandDispatcher
{
    public Task<Result<Void>> DispatchAsync<TCommand>(TCommand command)
    {
        var service = serviceProvider.GetRequiredService<ICommandHandler<TCommand>>();
        return service.HandleAsync(command);
    }
}
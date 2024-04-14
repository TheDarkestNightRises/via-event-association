using ViaEventAssociation.Core.Tools.OperationResult;
using Void = ViaEventAssociation.Core.Tools.OperationResult.Void;

namespace ViaEventAssociation.Core.Application.CommandDispatching.Dispatcher;

public interface ICommandDispatcher
{
    public Task<Result<Void>> DispatchAsync<TCommand>(TCommand command);
}
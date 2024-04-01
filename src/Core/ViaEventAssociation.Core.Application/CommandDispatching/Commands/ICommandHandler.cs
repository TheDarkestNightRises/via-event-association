using ViaEventAssociation.Core.Tools.OperationResult;
using Void = ViaEventAssociation.Core.Tools.OperationResult.Void;

namespace ViaEventAssociation.Core.Application.CommandDispatching.Commands;

public interface ICommandHandler<T>
{
    public Task<Result<Void>> HandleAsync(T command);
}
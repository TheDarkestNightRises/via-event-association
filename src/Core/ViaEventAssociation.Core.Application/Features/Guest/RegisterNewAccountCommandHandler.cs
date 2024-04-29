using ViaEventAssociation.Core.Application.CommandDispatching.Commands;
using ViaEventAssociation.Core.Application.CommandDispatching.Commands.Guest;
using ViaEventAssociation.Core.Tools.OperationResult;
using Void = ViaEventAssociation.Core.Tools.OperationResult.Void;

namespace ViaEventAssociation.Core.Application.Features.Guest;

public class RegisterNewAccountCommandHandler : ICommandHandler<RegisterNewAccountCommand>
{
    public Task<Result<Void>> HandleAsync(RegisterNewAccountCommand command)
    {
        throw new NotImplementedException();
    }
}
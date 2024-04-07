using ViaEventAssociation.Core.Application.CommandDispatching.Commands;
using ViaEventAssociation.Core.Application.CommandDispatching.Commands.Event;
using ViaEventAssociation.Core.Tools.OperationResult;
using Void = ViaEventAssociation.Core.Tools.OperationResult.Void;

namespace UnitTests.Common.Dispatcher;

public class MakeEventPublicMockHandler : ICommandHandler<MakeEventPublicCommand>
{
    public bool HasBeenCalled { get; set; }
    public int HowManyTimesIsCalled { get; set; } = 0;
    public async Task<Result<Void>> HandleAsync(MakeEventPublicCommand command)
    {
        HowManyTimesIsCalled++;
        HasBeenCalled = true;
        return new Void();
    }
}
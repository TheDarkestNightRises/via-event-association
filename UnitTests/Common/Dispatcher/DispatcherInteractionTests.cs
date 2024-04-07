using Microsoft.Extensions.DependencyInjection;
using ViaEventAssociation.Core.Application.CommandDispatching.Commands;
using ViaEventAssociation.Core.Application.CommandDispatching.Commands.Event;
using ViaEventAssociation.Core.Application.CommandDispatching.Dispatcher;
using ViaEventAssociation.Core.Application.Features.Event;

namespace UnitTests.Common.Dispatcher;

public class DispatcherInteractionTests
{
    private MakeEventPrivateCommand _cmd;
    private ICommandDispatcher _commandDispatcher;
    private IServiceProvider _serviceProvider;

    internal void SetupWithOneCommand()
    {
        var eventId = Guid.NewGuid();
        _cmd = MakeEventPrivateCommand.Create(eventId.ToString()).PayLoad;
        IServiceCollection serviceCollection = new ServiceCollection();
        serviceCollection.AddScoped<ICommandHandler<MakeEventPrivateCommand>, MakeEventPrivateMockHandler>();
        _serviceProvider = serviceCollection.BuildServiceProvider();
        _commandDispatcher = new CommandDispatcher(_serviceProvider);
    }

    //Zero
    [Fact]
    public async Task GivenZeroCommand_WhenDispatch_ThenSuccess()
    {
        SetupWithOneCommand();
        
        var result = await _commandDispatcher.DispatchAsync(_cmd);

        Assert.True(result.IsSuccess);
        var handler =
            (MakeEventPrivateMockHandler)_serviceProvider.GetService<ICommandHandler<MakeEventPrivateCommand>>()!;
        Assert.False(handler.HasBeenCalled);
    }
    
    //One - correct
    [Fact]
    public async Task GivenOneCorrectCommand_WhenDispatch_ThenSuccess()
    {
        SetupWithOneCommand();
        
        var result = await _commandDispatcher.DispatchAsync(_cmd);

        Assert.True(result.IsSuccess);
        var handler =
            (MakeEventPrivateMockHandler)_serviceProvider.GetService<ICommandHandler<MakeEventPrivateCommand>>()!;
        Assert.True(handler.HasBeenCalled);
    }
    
    [Fact]
    public async Task GivenOneCorrectCommand_WhenDispatch_ThenIsCalledOnce()
    {
        var result = await _commandDispatcher.DispatchAsync(_cmd);

        Assert.True(result.IsSuccess);
        var handler =
            (MakeEventPrivateMockHandler)_serviceProvider.GetService<ICommandHandler<MakeEventPrivateCommand>>()!;
        Assert.Equal(1, handler.HowManyTimesIsCalled);
    }
    
    //One - inccorrect
    
    //Multiple
    
    //Multiple with incorrect
    
}
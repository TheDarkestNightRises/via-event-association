using Microsoft.Extensions.DependencyInjection;
using ViaEventAssociation.Core.Application.CommandDispatching.Commands;
using ViaEventAssociation.Core.Application.CommandDispatching.Commands.Event;
using ViaEventAssociation.Core.Application.CommandDispatching.Dispatcher;
using ViaEventAssociation.Core.Application.Features.Event;

namespace UnitTests.Common.Dispatcher;

public class DispatcherInteractionTests
{
    private MakeEventPrivateCommand _cmd;
    private MakeEventPublicCommand _cmdPublic;
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

    internal void SetupWithTwoCommandIncorrect()
    {
        var eventId = Guid.NewGuid();
        _cmd = MakeEventPrivateCommand.Create(eventId.ToString()).PayLoad;
        IServiceCollection serviceCollection = new ServiceCollection();
        serviceCollection.AddScoped<ICommandHandler<MakeEventPublicCommand>, MakeEventPublicMockHandler>();
        serviceCollection.AddScoped<ICommandHandler<MakeEventReadyCommand>, MakeEventReadyCommandHandler>();
        _serviceProvider = serviceCollection.BuildServiceProvider();
        _commandDispatcher = new CommandDispatcher(_serviceProvider);
    }

    internal void SetupWithTwoHandlersCorrect()
    {
        var eventId = Guid.NewGuid();
        _cmd = MakeEventPrivateCommand.Create(eventId.ToString()).PayLoad;
        IServiceCollection serviceCollection = new ServiceCollection();
        serviceCollection.AddScoped<ICommandHandler<MakeEventPrivateCommand>, MakeEventPrivateMockHandler>();
        serviceCollection.AddScoped<ICommandHandler<MakeEventPublicCommand>, MakeEventPublicMockHandler>();
        _serviceProvider = serviceCollection.BuildServiceProvider();
        _commandDispatcher = new CommandDispatcher(_serviceProvider);
    }

    internal void SetupWithTwoHandlersDifferentCommandCorrect()
    {
        var eventId = Guid.NewGuid();
        _cmdPublic = MakeEventPublicCommand.Create(eventId.ToString()).PayLoad;
        IServiceCollection serviceCollection = new ServiceCollection();
        serviceCollection.AddScoped<ICommandHandler<MakeEventPrivateCommand>, MakeEventPrivateMockHandler>();
        serviceCollection.AddScoped<ICommandHandler<MakeEventPublicCommand>, MakeEventPublicMockHandler>();
        _serviceProvider = serviceCollection.BuildServiceProvider();
        _commandDispatcher = new CommandDispatcher(_serviceProvider);
    }

    internal void SetupWithZeroCommands()
    {
        IServiceCollection serviceCollection = new ServiceCollection();
        _serviceProvider = serviceCollection.BuildServiceProvider();
        _commandDispatcher = new CommandDispatcher(_serviceProvider);
    }

    //Zero 
    [Fact]
    public async Task GivenZeroCommand_WhenDispatch_ThenThrowInvalidException()
    {
        SetupWithZeroCommands();

        await Assert.ThrowsAsync<InvalidOperationException>(async () => await _commandDispatcher.DispatchAsync(_cmd));
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
        SetupWithOneCommand();

        var result = await _commandDispatcher.DispatchAsync(_cmd);

        Assert.True(result.IsSuccess);
        var handler =
            (MakeEventPrivateMockHandler)_serviceProvider.GetService<ICommandHandler<MakeEventPrivateCommand>>()!;
        Assert.Equal(1, handler.HowManyTimesIsCalled);
    }

    //One - incorrect
    [Fact]
    public async Task GivenOneIncorrectCommand_WhenDispatch_ThenIsCalledOnce()
    {
        SetupWithOneCommand();
        var cmd = MakeEventPublicCommand.Create(Guid.NewGuid().ToString()).PayLoad;
        await Assert.ThrowsAsync<InvalidOperationException>(async () => await _commandDispatcher.DispatchAsync(cmd));
    }

    //Multiple - make event private
    [Fact]
    public async Task GivenOneCommandAndTwoCorrectHandlers_WhenDispatch_ThenSuccess()
    {
        SetupWithTwoHandlersCorrect();

        var result = await _commandDispatcher.DispatchAsync(_cmd);

        Assert.True(result.IsSuccess);
        var handler =
            (MakeEventPrivateMockHandler)_serviceProvider.GetService<ICommandHandler<MakeEventPrivateCommand>>()!;
        Assert.True(handler.HasBeenCalled);
    }

    [Fact]
    public async Task GivenOneCommandAndTwoCorrectHandlers_WhenDispatch_ThenIsCalledOnce()
    {
        SetupWithTwoHandlersCorrect();

        var result = await _commandDispatcher.DispatchAsync(_cmd);

        Assert.True(result.IsSuccess);
        var handler =
            (MakeEventPrivateMockHandler)_serviceProvider.GetService<ICommandHandler<MakeEventPrivateCommand>>()!;
        Assert.Equal(1, handler.HowManyTimesIsCalled);
    }

    //Multiple - make event public
    [Fact]
    public async Task GivenAnotherCommandAndTwoCorrectHandlers_WhenDispatch_ThenSuccess()
    {
        SetupWithTwoHandlersDifferentCommandCorrect();

        var result = await _commandDispatcher.DispatchAsync(_cmdPublic);

        Assert.True(result.IsSuccess);
        var handler =
            (MakeEventPublicMockHandler)_serviceProvider.GetService<ICommandHandler<MakeEventPublicCommand>>()!;
        Assert.True(handler.HasBeenCalled);
    }

    [Fact]
    public async Task GivenAnnotherCommandAndTwoCorrectHandlers_WhenDispatch_ThenIsCalledOnce()
    {
        SetupWithTwoHandlersDifferentCommandCorrect();

        var result = await _commandDispatcher.DispatchAsync(_cmdPublic);

        Assert.True(result.IsSuccess);
        var handler =
            (MakeEventPublicMockHandler)_serviceProvider.GetService<ICommandHandler<MakeEventPublicCommand>>()!;
        Assert.Equal(1, handler.HowManyTimesIsCalled);
    }

    [Fact]
    public async Task GivenOneCommandAndTwoIncorrectHandlers_WhenDispatch_ThenFailure()
    {
        SetupWithTwoCommandIncorrect();

        await Assert.ThrowsAsync<InvalidOperationException>(async () => await _commandDispatcher.DispatchAsync(_cmd));
    }

    [Fact]
    public async Task GivenOneCommandAndTwoIncorrectHandlers_WhenDispatch_ThenNeverCalled()
    {
        SetupWithTwoCommandIncorrect();

        await Assert.ThrowsAsync<InvalidOperationException>(async () => await _commandDispatcher.DispatchAsync(_cmd));
    }
    
}
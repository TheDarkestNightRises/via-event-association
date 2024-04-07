using Microsoft.Extensions.DependencyInjection;
using ViaEventAssociation.Core.Application.CommandDispatching.Commands;
using ViaEventAssociation.Core.Application.CommandDispatching.Commands.Event;
using ViaEventAssociation.Core.Application.CommandDispatching.Dispatcher;

namespace UnitTests.Common.Dispatcher;

public class CommandWatchTimeTests
{
    private MakeEventPrivateCommand _cmd;
    private ICommandDispatcher _commandDispatcher;
    private CommandWatchTime _watch;
    private IServiceProvider _serviceProvider;

    public CommandWatchTimeTests()
    {
        var eventId = Guid.NewGuid();
        _cmd = MakeEventPrivateCommand.Create(eventId.ToString()).PayLoad;
        IServiceCollection serviceCollection = new ServiceCollection();
        serviceCollection.AddScoped<ICommandHandler<MakeEventPrivateCommand>, MakeEventPrivateMockHandler>();
        _serviceProvider = serviceCollection.BuildServiceProvider();
        _commandDispatcher = new CommandDispatcher(_serviceProvider);
        _watch = new CommandWatchTime(_commandDispatcher);
    }
    
    [Fact]
    public async Task DispatchAsync_ShouldMeasureTime()
    {

        // Act
        var result = await _watch.DispatchAsync(_cmd);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotEqual(TimeSpan.Zero, _watch.ElapsedTime);
    }

}
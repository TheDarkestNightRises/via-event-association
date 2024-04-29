using Microsoft.EntityFrameworkCore;
using UnitTests.Features.Event;
using ViaEventAssociation.Infrastructure.EfcDmPersistence.EventPersistence;
using Xunit;

namespace IntegrationTests.Repositories;

public class EventRepositoryTests : IClassFixture<EventSeedDataFixture>
{
    private readonly EventSeedDataFixture _fixture;
    private readonly EventRepository _repository;

    public EventRepositoryTests(EventSeedDataFixture fixture)
    {
        _fixture = fixture;
        _repository = new EventRepository(_fixture.Context);
    }

    [Fact]
    public async void GetAsync_When_EventsStored_Then_RetrieveEventById()
    {
        // Arrange
        var eventAggregate = await _fixture.Context.Events.FirstAsync();

        // Act
        var result = await _repository.GetAsync(eventAggregate.Id);

        // Assert
        Assert.Equal(eventAggregate, result);
    }
    
    [Fact]
    public async void RemoveAsync_When_EventNotStored_Then_EventIsStored()
    {
        // Arrange
        var eventAggregate = await _fixture.Context.Events.FirstAsync();

        // Act
        await _repository.RemoveAsync(eventAggregate.Id);
        await _fixture.Context.SaveChangesAsync();
        
        // Assert
        var removedEvent = await _fixture.Context.Events.FindAsync(eventAggregate.Id);
        Assert.Null(removedEvent);
    }
    
    [Fact]
    public async void AddAsync_When_EventsStored_Then_EventIsRemoved()
    {
        // Arrange
        var validEvent = EventFactory.RandomIdValidEvent();

        // Act
        await _repository.AddAsync(validEvent);
        await _fixture.Context.SaveChangesAsync();

        // Assert
        Assert.Single(await _fixture.Context.Events.ToListAsync());
    }
}
using IntegrationTests.Repositories;
using ViaEventAssociation.Core.QueryContracts.Queries;
using ViaEventAssociation.Infrastructure.EfcQueries.Queries;
using Xunit;

namespace IntegrationTests.Queries;

public class UnpublishedEventsQueryTests : IClassFixture<DataFixture>
{
    
    private readonly DataFixture _fixture;
    private readonly UnpublishedEventsPageQueryHandler _queryHandler;

    public UnpublishedEventsQueryTests(DataFixture fixture)
    {
        _fixture = fixture;
        _queryHandler = new UnpublishedEventsPageQueryHandler(_fixture.Context);
    }

    [Fact]
    public async Task GivenUnpublishedEventsQuery_WhenRequestingData_ReturnsCorrectCanceledEvents()
    {
        // Arrange
        var query = new UnpublishedEvents.Query();

        var expectedCanceledEvents = _fixture.Context.Events
            .Where(e => e.Status == "cancelled")
            .Select(e => new UnpublishedEvents.EventDetails(e.Title))
            .ToList();

        // Act
        var result = await _queryHandler.HandleAsync(query);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.CanceledEvents);
        Assert.Equal(expectedCanceledEvents, result.CanceledEvents);
    }

    [Fact]
    public async Task GivenUnpublishedEventsQuery_WhenRequestingData_ReturnsCorrectDraftEvents()
    {
        // Arrange
        var query = new UnpublishedEvents.Query();

        var expectedDraftEvents = _fixture.Context.Events
            .Where(e => e.Status == "draft")
            .Select(e => new UnpublishedEvents.EventDetails(e.Title))
            .ToList();

        // Act
        var result = await _queryHandler.HandleAsync(query);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.DraftEvents);
        Assert.Equal(expectedDraftEvents, result.DraftEvents);
    }

    [Fact]
    public async Task GivenUnpublishedEventsQuery_WhenRequestingData_ReturnsCorrectReadyEvents()
    {
        // Arrange
        var query = new UnpublishedEvents.Query();

        var expectedReadyEvents = _fixture.Context.Events
            .Where(e => e.Status == "ready")
            .Select(e => new UnpublishedEvents.EventDetails(e.Title))
            .ToList();

        // Act
        var result = await _queryHandler.HandleAsync(query);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.ReadyEvents);
        Assert.Equal(expectedReadyEvents, result.ReadyEvents);
    }

}
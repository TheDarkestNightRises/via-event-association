using IntegrationTests.Repositories;
using ViaEventAssociation.Core.QueryContracts.Queries;
using ViaEventAssociation.Infrastructure.EfcQueries.Queries;
using Xunit;

namespace IntegrationTests.Queries;

public class UpcomingEventQueryTest : IClassFixture<DataFixture>
{
    
    private readonly DataFixture _fixture;
    private readonly UpcomingEventsPageQueryHandler _queryHandler;

    public UpcomingEventQueryTest(DataFixture fixture)
    {
        _fixture = fixture;
        _queryHandler = new UpcomingEventsPageQueryHandler(_fixture.Context);
    }

    
    // Test id
    [Fact]
    public async Task GetAsync_GuestId_ReturnsGuest()
    {
        // Arrange
        
        // Act
        var result = await _queryHandler.HandleAsync(new UpcomingEventPage.Query(1, 1, "EventName"));

        // Assert
        Assert.NotNull(result);
    }
}
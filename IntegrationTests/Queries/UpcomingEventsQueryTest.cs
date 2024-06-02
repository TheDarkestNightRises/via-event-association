using IntegrationTests.Repositories;
using ViaEventAssociation.Core.QueryContracts.Queries;
using ViaEventAssociation.Infrastructure.EfcQueries.Queries;
using Xunit;

namespace IntegrationTests.Queries;

public class UpcomingEventsQueryTest : IClassFixture<DataFixture>
{
    
    private readonly DataFixture _fixture;
    private readonly UpcomingEventsPageQueryHandler _queryHandler;

    public UpcomingEventsQueryTest(DataFixture fixture)
    {
        _fixture = fixture;
        _queryHandler = new UpcomingEventsPageQueryHandler(_fixture.Context);
    }

    
    [Fact]
    public async Task GivenEventPageQuery_WhenRequestingPageData_ReturnsNotNull()
    {
        // Arrange
        var query = new UpcomingEventPage.Query(1, 1, "Soap Carving");
        
        // Act
        var result = await _queryHandler.HandleAsync(query);
        
        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.EventOverviews);
    }
}
using ViaEventAssociation.Core.QueryContracts.Queries;
using ViaEventAssociation.Infrastructure.EfcQueries.Queries;
using ViaEventAssociation.Infrastructure.EfcQueries.Scaffold;
using Xunit;

namespace IntegrationTests.Queries;

public class ViewSingleEventQueryTest : IClassFixture<DataFixture>
{
    private readonly DataFixture _fixture;
    private readonly ViewSingleEventQueryHandler _queryHandler;

    public ViewSingleEventQueryTest(DataFixture fixture)
    {
        _fixture = fixture;
        _queryHandler = new ViewSingleEventQueryHandler(_fixture.Context);
    }
    
    [Fact]
    public async Task GivenEventPageQuery_WhenRequestingPageData_ReturnsNotNull()
    {
        // Arrange
        var query = new ViewSingleEvent.Query("40ed2fd9-2240-4791-895f-b9da1a1f64e4");
        
        var expectedEvent = new Event
        {
            Title = "Friday Bar",
            Description = "Come for the cheap beer and great company.",
            Start = "2024-03-01 15:00",
            End = "2024-03-01 21:00",
            Visibility = "public",
            Status = "active",
        };
        
        // Act
        var result = await _queryHandler.HandleAsync(query);
        var foundEvent = result.SingleEvent;
        
        // Assert
        Assert.NotNull(foundEvent);
        Assert.Equal(expectedEvent.Title, foundEvent.Title);
        Assert.Equal(expectedEvent.Description, foundEvent.Description);
        Assert.Equal(expectedEvent.Start, foundEvent.Start);
        Assert.Equal(expectedEvent.End, foundEvent.End);
        Assert.Equal(expectedEvent.Visibility, foundEvent.Visibility);
    }

}
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
    public async Task GivenUnpublishedEventsQuery_WhenRequestingData_ReturnsNotNull()
    {
        // Arrange
        var query = new UnpublishedEvents.Query();
        
        // Act
        var result = await _queryHandler.HandleAsync(query);

        // Assert
        Assert.NotNull(result);
    }
}
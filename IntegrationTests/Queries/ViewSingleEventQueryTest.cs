using ViaEventAssociation.Core.QueryContracts.Queries;
using ViaEventAssociation.Infrastructure.EfcQueries.Queries;
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
        var query = new ViewSingleEvent.Query("1");
        
        // Act
        var result = await _queryHandler.HandleAsync(query);

        // Assert
        Assert.NotNull(result);
    }

}
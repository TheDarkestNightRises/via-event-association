using ViaEventAssociation.Core.QueryContracts.Queries;
using ViaEventAssociation.Infrastructure.EfcQueries.Queries;
using Xunit;

namespace IntegrationTests.Queries;

public class PersonalProfileQueryTest : IClassFixture<DataFixture>
{
    private readonly DataFixture _fixture;
    private readonly PersonalProfilePageQueryHandler _queryHandler;

    public PersonalProfileQueryTest(DataFixture fixture)
    {
        _fixture = fixture;
        _queryHandler = new PersonalProfilePageQueryHandler(_fixture.Context);
    }

    
    // Test id
    [Fact]
    public async Task GetAsync_GuestId_ReturnsGuest()
    {
        // Arrange
        string expectedEmail = "";
        string expectedFirstName = "";
        string expectedLastName = "";
        
        // Act
        var result = await _queryHandler.HandleAsync(new PersonalProfilePage.Query("34403069-d5a4-4c12-88e2-7ead0f377c81"));

        // Assert
        Assert.NotNull(result);
    }
}
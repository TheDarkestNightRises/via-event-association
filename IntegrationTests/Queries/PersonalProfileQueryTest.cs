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

    
    [Fact]
    public async Task GetAsync_GuestId_ReturnsGuest()
    {
        // Arrange
        var expectedEmail = "283529@via.dk";
        var expectedFullName = "Aanya Alvarado";
        
        var query = new PersonalProfilePage.Query("2e09e219-f919-46a8-91f7-e5a48874480b");
        
        // Act
        var result = await _queryHandler.HandleAsync(query);
        var personalProfile = result.PersonalProfile;
        
        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedEmail, personalProfile.Email);
        Assert.Equal(expectedFullName, personalProfile.FullName);
    }
}
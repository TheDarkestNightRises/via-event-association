using System.Net;
using System.Net.Http.Json;
using IntegrationTests.Abstractions;
using ViaEventAssociation.Presentation.WebAPI.Endpoints.Queries;
using Xunit;

namespace IntegrationTests.Endpoints.Queries;

public class PersonalPageEndpoint : BaseFunctionalTest
{
    
    public PersonalPageEndpoint(ViaWebApplicationFactory factory) : base(factory)
    {
    }
    
    [Fact]
    public async Task PersonalPageEndpoint_NoInput_ShouldReturnOk()
    {
        const string id = "5893604a-5eff-46c4-8056-77161a6e9665";
        var createdResponse = await Client.GetAsync($"api/personalPage/{id}");
        var createdEventResponse = await createdResponse.Content.ReadFromJsonAsync<UnpublishedEventsResponse>();

        Assert.True(createdResponse.StatusCode == HttpStatusCode.OK);
        Assert.NotNull(createdEventResponse);
    }
}
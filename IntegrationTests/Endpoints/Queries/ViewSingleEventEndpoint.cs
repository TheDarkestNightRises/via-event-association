using System.Net;
using System.Net.Http.Json;
using IntegrationTests.Abstractions;
using ViaEventAssociation.Presentation.WebAPI.Endpoints.Queries;
using Xunit;

namespace IntegrationTests.Endpoints.Queries;

public class ViewSingleEventEndpoint : BaseFunctionalTest
{
    public ViewSingleEventEndpoint(ViaWebApplicationFactory factory) : base(factory)
    {
    }
    
    [Fact]
    public async Task ViewSingleEventEndpoint_NoInput_ShouldReturnOk()
    {
        const string id = "0f8fad5b-d9cb-469f-a165-70867728950e";
        var createdResponse = await Client.GetAsync($"api/events/{id}");
        var createdEventResponse = await createdResponse.Content.ReadFromJsonAsync<UnpublishedEventsResponse>();

        Assert.True(createdResponse.StatusCode == HttpStatusCode.OK);
        Assert.NotNull(createdEventResponse);
    }
}
using System.Net;
using System.Net.Http.Json;
using IntegrationTests.Abstractions;
using ViaEventAssociation.Presentation.WebAPI.Endpoints.Queries;
using Xunit;

namespace IntegrationTests.Endpoints.Queries;

public class GetUnpublishedEventsEndpoint : BaseFunctionalTest
{
    public GetUnpublishedEventsEndpoint(ViaWebApplicationFactory factory) : base(factory)
    {
    }
    
    [Fact]
    public async Task UnpublishedEvents_NoInput_ShouldReturnOk()
    {
        var createdResponse = await Client.GetAsync("api/unpublished-events");
        var createdEventResponse = await createdResponse.Content.ReadFromJsonAsync<UnpublishedEventsResponse>();

        Assert.True(createdResponse.StatusCode == HttpStatusCode.OK);
        Assert.NotNull(createdEventResponse);
    }
}
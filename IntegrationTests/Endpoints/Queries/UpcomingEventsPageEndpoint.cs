using System.Net;
using System.Net.Http.Json;
using IntegrationTests.Abstractions;
using ViaEventAssociation.Presentation.WebAPI.Endpoints.Queries;
using Xunit;

namespace IntegrationTests.Endpoints.Queries;

public class UpcomingEventsPageEndpoint : BaseFunctionalTest
{
    public UpcomingEventsPageEndpoint(ViaWebApplicationFactory factory) : base(factory)
    {
    }
    
    [Fact]
    public async Task UpcomingEventsPageEndpoint_NoInput_ShouldReturnOk()
    {
        var createdResponse = await Client.GetAsync("api/upcoming-events");
        var createdEventResponse = await createdResponse.Content.ReadFromJsonAsync<UpcomingEventsResponse>();

        Assert.True(createdResponse.StatusCode == HttpStatusCode.OK);
        Assert.NotNull(createdEventResponse);
    }
}
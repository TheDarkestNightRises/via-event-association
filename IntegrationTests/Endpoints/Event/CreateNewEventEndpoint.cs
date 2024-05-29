using System.Net;
using System.Net.Http.Json;
using IntegrationTests.Abstractions;
using ViaEventAssociation.Presentation.WebAPI.Endpoints.Event.CreateNewEventEndpoint;
using ViaEventAssociation.Presentation.WebAPI.Endpoints.Queries;
using Xunit;

namespace IntegrationTests.Endpoints.Event;

public class CreateNewEventEndpoint : BaseFunctionalTest
{
    public CreateNewEventEndpoint(ViaWebApplicationFactory factory) : base(factory)
    {
        
    }
    
    [Fact]
    public async Task CreateEvent_ValidInput_ShouldReturnOk()
    {
        var createdResponse = await Client.PostAsync("/events/create/", null);
        var createdEventResponse = await createdResponse.Content.ReadFromJsonAsync<CreateNewEventResponse>();
        
        Assert.True(createdResponse.StatusCode == HttpStatusCode.OK);
        Assert.NotNull(createdEventResponse);
    }
    
    [Fact]
    public async Task CreateEvent2_ValidInput_ShouldReturnOk()
    {
        var createdResponse = await Client.GetAsync("/unpublished-events");
        var createdEventResponse = await createdResponse.Content.ReadFromJsonAsync<UnpublishedEventsResponse>();

        Assert.True(createdResponse.StatusCode == HttpStatusCode.OK);
        Assert.NotNull(createdEventResponse);
    }
}
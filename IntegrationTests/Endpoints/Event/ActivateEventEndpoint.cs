using System.Net;
using System.Net.Http.Json;
using IntegrationTests.Abstractions;
using ViaEventAssociation.Presentation.WebAPI.Endpoints.Event.ActivateEventEndpoint;
using ViaEventAssociation.Presentation.WebAPI.Endpoints.Event.CreateNewEventEndpoint;
using Xunit;

namespace IntegrationTests.Endpoints.Event;

public class ActivateEventEndpoint : BaseFunctionalTest
{
    public ActivateEventEndpoint(ViaWebApplicationFactory factory) : base(factory)
    {
    }
    
    [Fact]
    public async Task ActivateEvent_ValidInput_ShouldReturnOk()
    {
        var request = new ActivateEventRequest("");
        var createdResponse = await Client.PostAsJsonAsync("/events/activate-event", request);
        
        Assert.True(createdResponse.StatusCode == HttpStatusCode.OK);
    }
    
    [Fact]
    public async Task ActivateEvent_InvalidInput_ShouldReturnBadRequest()
    {
        var request = new ActivateEventRequest("c78d4475-4c0c-48d3-97e9-a494b69c1b51"); //Event does not exist
        var response = await Client.PostAsJsonAsync("/events/activate-event", request);
        
        Assert.False(response.StatusCode == HttpStatusCode.OK);
    }

}
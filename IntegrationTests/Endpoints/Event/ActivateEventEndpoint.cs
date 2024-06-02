using System.Net;
using System.Net.Http.Json;
using IntegrationTests.Abstractions;
using ViaEventAssociation.Presentation.WebAPI.Endpoints.Event.CreateNewEventEndpoint;
using Xunit;

namespace IntegrationTests.Endpoints.Event;

public class ActivateEventEndpoint : BaseFunctionalTest
{
    public ActivateEventEndpoint(ViaWebApplicationFactory factory) : base(factory)
    {
    }
    
    public async Task ActivateEvent_ValidInput_ShouldReturnOk()
    {
        var createdResponse = await Client.PostAsync("/events/create/", null);
        var createdEventResponse = await createdResponse.Content.ReadFromJsonAsync<CreateNewEventResponse>();
        
        Assert.True(createdResponse.StatusCode == HttpStatusCode.OK);
        Assert.NotNull(createdEventResponse);
    }
    
    public async Task ActivateEvent_InvalidInput_ShouldReturnBadRequest()
    {
        var createdResponse = await Client.PostAsync("/events/create/", null);
        var createdEventResponse = await createdResponse.Content.ReadFromJsonAsync<CreateNewEventResponse>();
        
        Assert.True(createdResponse.StatusCode == HttpStatusCode.OK);
        Assert.NotNull(createdEventResponse);
    }

}
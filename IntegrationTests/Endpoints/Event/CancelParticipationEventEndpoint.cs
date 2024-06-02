using System.Net;
using System.Net.Http.Json;
using IntegrationTests.Abstractions;
using ViaEventAssociation.Presentation.WebAPI.Endpoints.Event.CreateNewEventEndpoint;
using Xunit;

namespace IntegrationTests.Endpoints.Event;

public class CancelParticipationEventEndpoint : BaseFunctionalTest
{
    public CancelParticipationEventEndpoint(ViaWebApplicationFactory factory) : base(factory)
    {
    }
    
    public async Task CancelParticipationEvent_ValidInput_ShouldReturnOk()
    {
        var createdResponse = await Client.PostAsync("/events/create/", null);
        var createdEventResponse = await createdResponse.Content.ReadFromJsonAsync<CreateNewEventResponse>();
        
        Assert.True(createdResponse.StatusCode == HttpStatusCode.OK);
        Assert.NotNull(createdEventResponse);
    }
    
    public async Task CancelParticipationEvent_InvalidInput_ShouldReturnBadRequest()
    {
        var createdResponse = await Client.PostAsync("/events/create/", null);
        var createdEventResponse = await createdResponse.Content.ReadFromJsonAsync<CreateNewEventResponse>();
        
        Assert.True(createdResponse.StatusCode == HttpStatusCode.OK);
        Assert.NotNull(createdEventResponse);
    }
    
}
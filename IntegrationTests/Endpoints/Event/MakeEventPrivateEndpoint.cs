using System.Net;
using System.Net.Http.Json;
using IntegrationTests.Abstractions;
using Microsoft.EntityFrameworkCore;
using ViaEventAssociation.Core.Domain.Aggregates.Event.Values;
using ViaEventAssociation.Presentation.WebAPI.Endpoints.Event.ActivateEventEndpoint;
using ViaEventAssociation.Presentation.WebAPI.Endpoints.Event.MakeEventPrivateEndpoint;
using Xunit;

namespace IntegrationTests.Endpoints.Event;

public class MakeEventPrivateEndpoint : BaseFunctionalTest
{
    
    public MakeEventPrivateEndpoint(ViaWebApplicationFactory factory) : base(factory)
    {
    }
    
    [Fact]
    public async Task MakeEventPrivateEndpoint_ValidInput_ShouldReturnOk()
    {
        const string id = "0f8fad5b-d9cb-469f-a165-70867728950e";
        var request = new MakeEventPrivateRequest(id); 
        var createdResponse = await Client.PostAsJsonAsync("api/events/private-event", request);
        var eventId = EventId.FromString(id).PayLoad;
        var viaEvent = await DmContext.Events.FirstOrDefaultAsync(e => e.Id == eventId);
        Assert.True(createdResponse.StatusCode == HttpStatusCode.NoContent);
        Assert.True(viaEvent.EventVisibility == EventVisibility.Private);
    }
    
    [Fact]
     public async Task MakeEventPrivateEndpoint_InvalidInput_ShouldReturnBadRequest()
     {
         const string id = "27bd6ad3-24ba-4e54-b299-c4b16ed1c127"; // Canceled event
         var request = new MakeEventPrivateRequest(id); 
         var response = await Client.PostAsJsonAsync("api/events/private-event", request);
         
         Assert.False(response.StatusCode == HttpStatusCode.NoContent);
     }
}
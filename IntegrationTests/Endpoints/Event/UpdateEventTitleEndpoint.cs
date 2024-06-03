using System.Diagnostics;
using System.Net;
using System.Net.Http.Json;
using IntegrationTests.Abstractions;
using Microsoft.EntityFrameworkCore;
using ViaEventAssociation.Core.Domain.Aggregates.Event.Values;
using ViaEventAssociation.Presentation.WebAPI.Endpoints.Event.CreateNewEventEndpoint;
using ViaEventAssociation.Presentation.WebAPI.Endpoints.Event.UpdateEventTitleEndpoint;
using Xunit;

namespace IntegrationTests.Endpoints.Event;

public class UpdateEventTitleEndpoint : BaseFunctionalTest
{
    public UpdateEventTitleEndpoint(ViaWebApplicationFactory factory) : base(factory)
    {
    }
    
    [Fact]
    public async Task UpdateEventTitle_ValidInput_ShouldReturnOk()
    {
        //Arrange
        const string id = "0f8fad5b-d9cb-469f-a165-70867728950e";
        var newTitle = "damm";
        var request = new UpdateEventTitleRequest(id, newTitle);
        
        var response = await Client.PostAsJsonAsync("api/events/update-event-title", request);
        
        var eventId = EventId.FromString(id).PayLoad;
        var viaEvent = await DmContext.Events.FirstOrDefaultAsync(e => e.Id == eventId);
        Assert.NotNull(viaEvent);
        Assert.True(response.StatusCode == HttpStatusCode.NoContent);
        Assert.True(viaEvent.EventTitle.Title == newTitle);
    }
}
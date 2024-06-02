using System.Net;
using System.Net.Http.Json;
using IntegrationTests.Abstractions;
using ViaEventAssociation.Core.Domain.Aggregates.Event.Values;
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
    public async Task UpdateEventTitle_ValidInput_ShouldReturnOk()
    {
        var createdResponse = await Client.PostAsync("/events/create/", null);
        var createdEventResponse = await createdResponse.Content.ReadFromJsonAsync<CreateNewEventResponse>();
        Assert.NotNull(createdEventResponse);
        var eventId = EventId.FromGuid(createdEventResponse.Id);
        var viaEvent = DmContext.Events.SingleOrDefault(evt => evt.Id == eventId);
        Assert.NotNull(viaEvent);
        Assert.True(createdResponse.StatusCode == HttpStatusCode.OK);
    }
}
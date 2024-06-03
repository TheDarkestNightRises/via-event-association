// using System.Net;
// using System.Net.Http.Json;
// using IntegrationTests.Abstractions;
// using Microsoft.EntityFrameworkCore;
// using ViaEventAssociation.Core.Domain.Aggregates.Event.Values;
// using ViaEventAssociation.Presentation.WebAPI.Endpoints.Event.ActivateEventEndpoint;
// using ViaEventAssociation.Presentation.WebAPI.Endpoints.Event.CreateNewEventEndpoint;
// using Xunit;
//
// namespace IntegrationTests.Endpoints.Event;
//
// public class ActivateEventEndpoint : BaseFunctionalTest
// {
//     public ActivateEventEndpoint(ViaWebApplicationFactory factory) : base(factory)
//     {
//     }
//     
//     [Fact]
//     public async Task ActivateEvent_ValidInput_ShouldReturnOk()
//     {
//         const string id = "0f8fad5b-d9cb-469f-a165-70867728950e";
//         var request = new ActivateEventRequest(id); 
//         var createdResponse = await Client.PostAsJsonAsync("/events/activate-event", request);
//         var eventId = EventId.FromString(id).PayLoad;
//         var viaEvent = await DmContext.Events.FirstOrDefaultAsync(e => e.Id == eventId);
//         Assert.True(createdResponse.StatusCode == HttpStatusCode.OK);
//     }
//     
//     [Fact]
//     public async Task ActivateEvent_InvalidInput_ShouldReturnBadRequest()
//     {
//         var request = new ActivateEventRequest("c78d4475-4c0c-48d3-97e9-a494b69c1b51"); //Event does not exist
//         var response = await Client.PostAsJsonAsync("/events/activate-event", request);
//         
//         Assert.False(response.StatusCode == HttpStatusCode.OK);
//     }
//
// }
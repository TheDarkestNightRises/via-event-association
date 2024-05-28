using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using ViaEventAssociation.Presentation.WebAPI.Endpoints.Event.CreateNewEventEndpoint;
using Xunit;

namespace IntegrationTests.Endpoints.Event;

public class CreateNewEventEndpoint
{
    [Fact]
    public async Task UpdateTitle_ValidInput_ShouldReturnOk()
    {
        using WebApplicationFactory<Program> factory = new ViaWebApplicationFactory();
        var client = new HttpClient();

        var createdResponse = await client.PostAsync("api/events/create", JsonContent.Create(new { }));
        var createdEventResponse = await createdResponse.Content.ReadFromJsonAsync<CreateNewEventResponse>();
        
    }
}
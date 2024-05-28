using System.Net;
using System.Net.Http.Json;
using ViaEventAssociation.Presentation.WebAPI.Endpoints.Event.CreateNewEventEndpoint;
using ViaEventAssociation.Presentation.WebAPI.Endpoints.Queries;
using Xunit;

namespace IntegrationTests.Endpoints.Event;

public class CreateNewEventEndpoint : BaseIntegrationTest
{
    private ViaWebApplicationFactory _factory;
    private readonly HttpClient _client;
    
    public CreateNewEventEndpoint(ViaWebApplicationFactory factory) : base(factory)
    {
        _factory = factory;
    }
    
    [Fact]
    public async Task CreateEvent_ValidInput_ShouldReturnOk()
    {
        var client = _factory.CreateClient();

        var createdResponse = await client.PostAsync("/events/create/", JsonContent.Create(new { }));
        var createdEventResponse = await createdResponse.Content.ReadFromJsonAsync<CreateNewEventResponse>();

        Assert.True(createdResponse.StatusCode == HttpStatusCode.OK);
        Assert.NotNull(createdEventResponse);
    }
    
    [Fact]
    public async Task CreateEvent2_ValidInput_ShouldReturnOk()
    {
        var client = _factory.CreateClient();

        var createdResponse = await client.GetAsync("/unpublished-events");
        var createdEventResponse = await createdResponse.Content.ReadFromJsonAsync<UnpublishedEventsResponse>();

        Assert.True(createdResponse.StatusCode == HttpStatusCode.OK);
    }
}
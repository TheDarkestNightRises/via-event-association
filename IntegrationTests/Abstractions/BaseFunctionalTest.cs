using IntegrationTests.Endpoints;
using Xunit;

namespace IntegrationTests.Abstractions;

public class BaseFunctionalTest : IClassFixture<ViaWebApplicationFactory>
{
    protected HttpClient Client { get; init; }

    public BaseFunctionalTest(ViaWebApplicationFactory factory)
    {
        Client = factory.CreateClient();
    }
}
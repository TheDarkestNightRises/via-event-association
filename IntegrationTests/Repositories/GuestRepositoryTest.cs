using Microsoft.EntityFrameworkCore;
using UnitTests.Features.Guest;
using ViaEventAssociation.Core.Domain.Aggregates.Guest;
using ViaEventAssociation.Core.Domain.Aggregates.Guest.Values;
using ViaEventAssociation.Infrastructure.EfcDmPersistence.Context;
using ViaEventAssociation.Infrastructure.EfcDmPersistence.GuestPersistence;
using Xunit;


namespace IntegrationTests.Repositories;

public class GuestRepositoryTest : IClassFixture<GuestSeedDataFixture>
{
    private GuestSeedDataFixture _fixture;
    private GuestRepository _repository;

    public GuestRepositoryTest(GuestSeedDataFixture fixture)
    {
        _fixture = fixture;
        _repository = new GuestRepository(_fixture.Context);
    }

    // Test id
    [Fact]
    public async Task GetAsync_GuestId_ReturnsGuest()
    {
        // Arrange
        var guest = await _fixture.Context.Guests.FirstAsync();

        // Act
        var result = await _repository.GetAsync(guest.Id);

        // Assert
        Assert.Equal(guest, result);
    }

    // test remove
    [Fact]
    public async Task RemoveAsync_GuestId_RemovesGuest()
    {
        // Arrange
        var guest = await _fixture.Context.Guests.FirstAsync();

        // Act
        await _repository.RemoveAsync(guest.Id);

        // Assert
        Assert.Empty(_fixture.Context.Guests);
    }

    // Test add

    [Fact]
    public async Task AddAsync_Guest_ReturnsGuest()
    {
        // Arrange
        var guest = GuestFactory.ValidGuest();

        // Act
        await _repository.AddAsync(guest);

        // Assert
        Assert.Single(await _fixture.Context.Guests.ToListAsync());
    }
}


using FakeItEasy;
using ViaEventAssociation.Core.Tools.ObjectMapper;

namespace UnitTests.Common.ObjectMapperTests;

public class ObjectMapperTests
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ObjectMapper _mapper;

    public ObjectMapperTests()
    {
        _serviceProvider = A.Fake<IServiceProvider>();
        _mapper = new ObjectMapper(_serviceProvider);
    }

    [Fact]
    public void Map_UsesCustomMappingConfig_WhenConfigIsAvailable()
    {
        // Arrange
        var mappingConfig = A.Fake<IMappingConfig<Source, Destination>>();
        var source = new Source { Value = 42 };
        var destination = new Destination { Value = 84 };

        A.CallTo(() => mappingConfig.Map(source)).Returns(destination);
        A.CallTo(() => _serviceProvider.GetService(typeof(IMappingConfig<Source, Destination>)))
            .Returns(mappingConfig);

        // Act
        var result = _mapper.Map<Destination>(source);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(destination.Value, result.Value);
    }

    [Fact]
    public void Map_UsesJsonSerialization_WhenNoCustomMappingConfig()
    {
        // Arrange
        var source = new Source { Value = 42 };

        A.CallTo(() => _serviceProvider.GetService(A<Type>.Ignored)).Returns(null);

        // Act
        var result = _mapper.Map<Destination>(source);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(source.Value, result.Value);
    }

    public class Source
    {
        public int Value { get; set; }
    }

    public class Destination
    {
        public int Value { get; set; }
    }
}

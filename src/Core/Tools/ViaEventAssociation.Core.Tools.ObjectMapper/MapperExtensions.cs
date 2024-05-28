using Microsoft.Extensions.DependencyInjection;

namespace ViaEventAssociation.Core.Tools.ObjectMapper;

public static class MapperExtensions
{
    public static void RegisterMapper(this IServiceCollection services)
    {
        services.AddScoped<IMapper, ObjectMapper>();
    }

}
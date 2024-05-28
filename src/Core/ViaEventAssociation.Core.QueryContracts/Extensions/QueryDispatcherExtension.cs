using Microsoft.Extensions.DependencyInjection;
using ViaEventAssociation.Core.QueryContracts.QueryDispatching;

namespace ViaEventAssociation.Core.QueryContracts.Extensions;

public static class QueryDispatcherExtension
{
    public static void RegisterQueryDispatcher(this IServiceCollection services)
    {
        services.AddScoped<IQueryDispatcher, QueryDispatcher>();
    }
}

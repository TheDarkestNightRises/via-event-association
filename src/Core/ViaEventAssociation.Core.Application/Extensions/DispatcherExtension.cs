using Microsoft.Extensions.DependencyInjection;
using ViaEventAssociation.Core.Application.CommandDispatching.Dispatcher;

namespace ViaEventAssociation.Core.Application.Extensions;

public static class DispatcherExtension
{
    public static void RegisterDispatcher(this IServiceCollection services)
    {
        services.AddScoped<ICommandDispatcher, CommandDispatcher>();
    }
}
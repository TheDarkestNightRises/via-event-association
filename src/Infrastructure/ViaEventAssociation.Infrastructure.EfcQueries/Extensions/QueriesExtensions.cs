using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ViaEventAssociation.Core.QueryContracts.Contract;
using ViaEventAssociation.Core.QueryContracts.Queries;
using ViaEventAssociation.Infrastructure.EfcQueries.Context;
using ViaEventAssociation.Infrastructure.EfcQueries.Queries;

namespace ViaEventAssociation.Infrastructure.EfcQueries.Extensions;

public static class QueriesExtensions
{
    public static void RegisterQueryHandlers(this IServiceCollection services)
    {
        var databasePath =
            "../../Infrastructure/ViaEventAssociation.Infrastructure.EfcDmPersistence/VEADatabaseProduction.db";
        services.AddDbContext<VeadatabaseProductionContext>(options => { options.UseSqlite($"Data Source={databasePath}"); });
        services.AddScoped<IQueryHandler<PersonalProfilePage.Query, PersonalProfilePage.Answer>, PersonalProfilePageQueryHandler>();
        services.AddScoped<IQueryHandler<UnpublishedEvents.Query, UnpublishedEvents.Answer>, UnpublishedEventsPageQueryHandler>();
        services.AddScoped<IQueryHandler<UpcomingEventPage.Query, UpcomingEventPage.Answer>, UpcomingEventsPageQueryHandler>();
        services.AddScoped<IQueryHandler<ViewSingleEvent.Query, ViewSingleEvent.Answer>, ViewSingleEventQueryHandler>();
    }
}
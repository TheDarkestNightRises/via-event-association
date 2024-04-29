﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ViaEventAssociation.Core.Domain.Aggregates.Event.Repository;
using ViaEventAssociation.Core.Domain.Aggregates.Guest.Repository;
using ViaEventAssociation.Core.Domain.Common.UnitOfWork;
using ViaEventAssociation.Infrastructure.EfcDmPersistence.Context;
using ViaEventAssociation.Infrastructure.EfcDmPersistence.EventPersistence;
using ViaEventAssociation.Infrastructure.EfcDmPersistence.GuestPersistence;
using ViaEventAssociation.Infrastructure.EfcDmPersistence.UnitOfWork;

namespace ViaEventAssociation.Infrastructure.EfcDmPersistence.Extensions;

public static class RepositoryExtensions
{
    public static void RegisterRepository(this IServiceCollection services)
    {
        services.AddDbContext<DmContext>(options =>
        {
            options.UseSqlite(
                @"Data Source=C:\Github\C#\via-event-association\src\Infrastructure\ViaEventAssociation.Infrastructure.EfcDmPersistence\VEADatabaseProduction.db");
        });
        services.AddScoped<IUnitOfWork, SqliteUnitOfWork>();
        services.AddScoped<IEventRepository, EventRepository>();
        services.AddScoped<IGuestRepository, GuestRepository>();
    }
}
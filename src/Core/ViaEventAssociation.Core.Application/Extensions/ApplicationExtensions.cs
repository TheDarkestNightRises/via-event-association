using Microsoft.Extensions.DependencyInjection;
using ViaEventAssociation.Core.Application.CommandDispatching.Commands;
using ViaEventAssociation.Core.Application.CommandDispatching.Commands.Event;
using ViaEventAssociation.Core.Application.CommandDispatching.Commands.Guest;
using ViaEventAssociation.Core.Application.Features.Event;
using ViaEventAssociation.Core.Application.Features.Guest;

namespace ViaEventAssociation.Core.Application.Extensions;

public static class ApplicationExtensions
{
    public static void RegisterHandlers(this IServiceCollection services)
    {
        //Event
        services.AddScoped<ICommandHandler<CancelParticipationInEventCommand>, CancelParticipationInEventCommandHandler>();
        services.AddScoped<ICommandHandler<CreateNewEventCommand>, CreateNewEventCommandHandler>();
        services.AddScoped<ICommandHandler<CreatorActivatesAnEventCommand>, CreatorActivatesAnEventCommandHandler>();
        services.AddScoped<ICommandHandler<GuestIsInvitedToEventCommand>, GuestIsInvitedToEventCommandHandler>();
        services.AddScoped<ICommandHandler<MakeEventPrivateCommand>, MakeEventPrivateCommandHandler>();
        services.AddScoped<ICommandHandler<MakeEventPublicCommand>, MakeEventPublicCommandHandler>();
        services.AddScoped<ICommandHandler<MakeEventReadyCommand>, MakeEventReadyCommandHandler>();
        services.AddScoped<ICommandHandler<ParticipateInPublicEventCommand>, ParticipateInPublicEventCommandHandler>();
        services.AddScoped<ICommandHandler<SetMaxNumberOfGuestsCommand>, SetMaxNumberOfGuestsCommandHandler>();
        services.AddScoped<ICommandHandler<UpdateEventDescriptionCommand>, UpdateEventDescriptionCommandHandler>();
        services.AddScoped<ICommandHandler<UpdateEventTitleCommand>, UpdateEventTitleCommandHandler>();
        services.AddScoped<ICommandHandler<UpdateTimeIntervalCommand>, UpdateTimeIntervalCommandHandler>();
        
        //Guest
        services.AddScoped<ICommandHandler<RegisterNewAccountCommand>, RegisterNewAccountCommandHandler>();
        services.AddScoped<ICommandHandler<GuestAcceptsInvitationCommand>, GuestAcceptsInvitationCommandHandler>();
        services.AddScoped<ICommandHandler<GuestDeclinesInvitationCommand>, GuestDeclinesInvitationCommandHandler>();
    }
}
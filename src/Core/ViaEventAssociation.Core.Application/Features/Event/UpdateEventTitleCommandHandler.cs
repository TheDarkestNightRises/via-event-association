using ViaEventAssociation.Core.Application.CommandDispatching.Commands;
using ViaEventAssociation.Core.Application.CommandDispatching.Commands.Event;
using ViaEventAssociation.Core.Domain.Aggregates.Event.Repository;
using ViaEventAssociation.Core.Domain.Common.UnitOfWork;
using ViaEventAssociation.Core.Tools.OperationResult;
using Void = ViaEventAssociation.Core.Tools.OperationResult.Void;

namespace ViaEventAssociation.Core.Application.Features.Event;


    public class UpdateEventTitleCommandHandler(IEventRepository repository, IUnitOfWork unitOfWork) :ICommandHandler<UpdateEventTitleCommand>
    {
        public async Task<Result<Void>> HandleAsync(UpdateEventTitleCommand command)
        {
            var evnt = await repository.GetAsync(command.Id);
            
            var result = evnt.UpdateEventTitle(command.Title);

            if (result.IsFailure)
            {
                return result;
            }

            await unitOfWork.SaveChangesAsync();
            return new Void();
        }
    }

using ViaEventAssociation.Core.Application.CommandDispatching.Commands.Event;
using ViaEventAssociation.Core.Domain.Aggregates.Event.EventErrors;

namespace UnitTests.Features.Event.UpdateEventTitle;

public class UpdateEventTitleCommandTests
    {
        [Fact]
        public void GivenValidateTitle_WhenCreatingUpdateEventTitleCommand_ThenSuccess()
        {
            var title = "Event is cool";
            var guid = Guid.NewGuid().ToString();
            var result = UpdateEventTitleCommand.Create(guid, title);
            var command = result.PayLoad;

            Assert.True(result.IsSuccess);
            Assert.True((string) command.Title == title);
        }
    
        [Fact]
        public void GivenvalidTitle_WhenCreatingCommand_ThenErrorIsProvided()
        {
            var longTitle = new string('A', 251);
            var guid = Guid.NewGuid().ToString();
            var result = UpdateEventTitleCommand.Create(guid, longTitle);
            var command = result.PayLoad;

            Assert.True(result.IsFailure);
            Assert.Contains(result.Errors, error => error == EventAggregateErrors.TitleUpdateInputNotValid);
        }
}
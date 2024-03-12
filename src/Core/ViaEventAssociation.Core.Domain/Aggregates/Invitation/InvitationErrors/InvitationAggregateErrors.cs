using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Domain.Aggregates.Entity.InvitationErrors;

public static class InvitationAggregateErrors
{
    public static class Invitation
    {
        private const string Code = "Invitation.Invitation";
        
            public static Error Failed => new(Code, "Failed to send the invitation");
        }
    }

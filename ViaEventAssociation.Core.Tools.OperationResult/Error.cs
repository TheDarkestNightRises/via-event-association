namespace ViaEventAssociation.Core.Tools.OperationResult;

public sealed record Error(string Code, string? Description = null)
{
    public override string ToString() => $"Error {{ Code = {Code}, Description = {Description} }}";
}
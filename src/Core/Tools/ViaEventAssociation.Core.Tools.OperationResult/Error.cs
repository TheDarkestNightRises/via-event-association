namespace ViaEventAssociation.Core.Tools.OperationResult2;

public sealed record Error(string Code, string? Description = null)
{
    public override string ToString() => $"Error {{ Code = {Code}, Description = {Description} }}";
}
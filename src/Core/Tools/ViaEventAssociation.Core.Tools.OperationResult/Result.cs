namespace ViaEventAssociation.Core.Tools.OperationResult;

public class Result<T>
{
    public T? PayLoad;
    public List<Error> Errors = new();
    public bool IsFailure => Errors.Count != 0;
    public bool IsSuccess => !IsFailure;
    
    //Happy path
    public Result(T payLoad)
    {
        PayLoad = payLoad;
    } 
    
    //Failure path 
    private Result(Error error)
    {
        Errors.Add(error);
    }

    private Result(List<Error> errors)
    {
        Errors.AddRange(errors);
    }
    
    public static implicit operator Result<T>(T payload) => new(payload);
    public static implicit operator Result<T>(Error error) => new(error);
    public static implicit operator Result<T>(List<Error> errors) => new(errors);
    public static implicit operator Result<T>(Error[] errors) => new(errors.ToList());
}
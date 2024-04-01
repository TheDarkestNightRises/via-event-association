namespace ViaEventAssociation.Core.Tools.OperationResult;

public class Result<T>
{
    private readonly T? _payLoad = default;
    private readonly List<Error> _errors = [];
    public bool IsFailure => _errors.Count != 0;
    public bool IsSuccess => !IsFailure;
    
    //Happy path
    public Result(T payLoad)
    {
        _payLoad = payLoad;
    } 
    
    //Failure path 
    private Result(Error error)
    {
        _errors.Add(error);
    }

    private Result(List<Error> errors)
    {
        _errors.AddRange(errors);
    }
    public T PayLoad => _payLoad!;
    public List<Error> Errors => _errors;
    public static implicit operator Result<T>(T payload) => new(payload);
    public static implicit operator Result<T>(Error error) => new(error);
    public static implicit operator Result<T>(List<Error> errors) => new(errors);
    public static implicit operator Result<T>(Error[] errors) => new(errors.ToList());

    public TNextValue Match<TNextValue>(Func<T, TNextValue> onPayLoad, Func<List<Error>, TNextValue> onError)
    {
        if (IsFailure)
        {
            return onError(_errors);
        }

        return onPayLoad(PayLoad);
    }
    
    public Result<TNextValue> Then<TNextValue>(Func<T, TNextValue> onPayload)
    {
        if (IsFailure)
        {
            return Errors;
        }

        return onPayload(PayLoad);
    }
}
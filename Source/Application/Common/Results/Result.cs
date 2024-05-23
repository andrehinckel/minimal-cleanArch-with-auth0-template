namespace Application.Common.Results;

public class Result : BaseResult
{
    private Result(bool isSuccess)
    {
        IsSuccess = isSuccess;
    }

    public static Result Failure(IEnumerable<string> message)
    {
        return new Result(false).WithErrors(message);
    }
    
    public static Result Failure(IEnumerable<Error> errors)
    {
        return new Result(false).WithErrors(errors);
    }

    public static Result Failure(string message)
    {
        return new Result(false).WithError(message);
    }
    
    public static Result Failure()
    {
        return new Result(false);
    }
    
    public static Result Ok() => new(true);

    public static Result<T> Ok<T>(T data)
    {
        return new Result<T> { Data = data, IsSuccess = true };
    }
    
    public Result WithErrors(IEnumerable<string> message)
    {
        Errors.AddRange(message.Select(x => new Error(x)));
        return this;
    }
    
    private Result WithErrors(IEnumerable<Error> errors)
    {
        Errors.AddRange(errors);
        return this;
    }

    private Result WithError(string message)
    {
        Errors.Add(new Error(message));
        return this;
    }
}

public class Result<T> : BaseResult
{
    public new T? Data { get; init; }

    public static implicit operator Result<T>(Result result)
    {
        return new Result<T> { Errors = result.Errors, Data = default };
    }
}
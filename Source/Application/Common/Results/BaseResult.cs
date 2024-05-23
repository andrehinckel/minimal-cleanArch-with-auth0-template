namespace Application.Common.Results;

public abstract class BaseResult
{
    public bool IsSuccess { get; init; }
    public List<Error> Errors { get; init; } = [];
    public object? Data { get; }
}
using Application.Common.Results;

namespace Presentation.Extensions;

public static class ResultExtensions
{
    public static IResult ToResult(
        this Result result,
        IResult? onSuccess = null,
        IResult? onFailure = null)
    {
        if (result.IsSuccess)
            return onSuccess ?? Results.Ok(result);

        return onFailure ?? Results.BadRequest(result);
    }

    public static IResult ToResult<T>(
        this Result<T> result,
        IResult? onSuccess = null,
        IResult? onFailure = null)
    {
        if (result.IsSuccess)
            return onSuccess ?? Results.Ok(result);

        return onFailure ?? Results.BadRequest(result);
    }
}
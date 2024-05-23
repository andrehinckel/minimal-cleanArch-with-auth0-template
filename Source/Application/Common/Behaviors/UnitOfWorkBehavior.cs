using Application.Common.Cqrs;
using Domain.Common;
using MediatR;

namespace Application.Common.Behaviors;

public class UnitOfWorkBehavior<TRequest, TResponse>(IUnitOfWork unitOfWork) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>, ICommand
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        await unitOfWork.BeginTransactionAsync();

        try
        {
            var result = await next();
            await unitOfWork.CommitAsync();

            return result;
        }
        catch (Exception)
        {
            await unitOfWork.Rollback();
            throw;
        }
    }
}
using Application.Common.Results;
using MediatR;

namespace Application.Common.Cqrs;

public interface ICommand : IRequest<Result>;

public interface ICommand<T> : IRequest<Result<T>> where T : notnull;
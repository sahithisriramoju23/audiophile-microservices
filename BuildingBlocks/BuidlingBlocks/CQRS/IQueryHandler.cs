using MediatR;

namespace BuidlingBlocks.CQRS;

public interface IQueryHandler<in TRequest> : IRequestHandler<TRequest,Unit>
    where TRequest : IQuery<Unit>
{
}

public interface IQueryHandler<in TRequest, TResponse> : IRequestHandler<TRequest,TResponse>
    where TRequest : IQuery<TResponse>
    where TResponse : notnull
{
}

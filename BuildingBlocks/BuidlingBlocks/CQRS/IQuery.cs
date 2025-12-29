using MediatR;

namespace BuidlingBlocks.CQRS;

public interface IQuery<out TResponse> : IRequest<TResponse>
    where TResponse : notnull
{
}

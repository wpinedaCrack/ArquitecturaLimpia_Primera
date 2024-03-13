using CleanArchitecture.Domain.Abstractions;
using MediatR;

namespace CleanArchitecture.Aplication.Abstractions.Messaging;

public interface IQueryHandler<TQuery, TResponse>
: IRequestHandler<TQuery, Result<TResponse>>
where TQuery : IQuery<TResponse>
{

}

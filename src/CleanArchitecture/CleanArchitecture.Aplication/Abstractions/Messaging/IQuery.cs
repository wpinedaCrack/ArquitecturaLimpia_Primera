using CleanArchitecture.Domain.Abstractions;
using MediatR;

namespace CleanArchitecture.Aplication.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{

}

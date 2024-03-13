using CleanArchitecture.Domain.Abstractions;
using MediatR;

namespace CleanArchitecture.Aplication.Abstractions.Messaging;

public interface ICommand : IRequest<Result>, IBaseCommand
{

}

public interface ICommand<TResponse> : IRequest<Result<TResponse>>, IBaseCommand
{

}

public interface IBaseCommand
{ }
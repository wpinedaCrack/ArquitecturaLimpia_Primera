using CleanArchitecture.Domain.Abstractions;
using MediatR;

namespace CleanArchitecture.Aplication.Abstractions.Messaging;

//where 1=1 ==> Contraint generico
public interface ICommandHandler<TCommand> : IRequestHandler<TCommand, Result>
where TCommand : ICommand
{

}

public interface ICommandHandler<TCommand, TResponse>
: IRequestHandler<TCommand, Result<TResponse>>
where TCommand : ICommand<TResponse>
{

}
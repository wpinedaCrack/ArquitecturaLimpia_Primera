using CleanArchitecture.Aplication.Abstractions.Messaging;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Aplication.Abstractions.Behaviors;

public class LoggingBehavior<TRequest, TResponse>
: IPipelineBehavior<TRequest, TResponse>
where TRequest : IBaseCommand
{
    private readonly ILogger<TRequest> _logger;

    public LoggingBehavior(ILogger<TRequest> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken
        )
    {
        var name = request.GetType().Name;  //Nombre de la clase al cual pertenece el objeto request

        try
        {
            _logger.LogInformation($"Ejecutando el command request: {name}");
            var result = await next();
            _logger.LogInformation($"El comando {name} se ejecuto exitosamente");

            return result;
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, $"El comando {name} tuvo errores");
            throw;
        }

    }
}

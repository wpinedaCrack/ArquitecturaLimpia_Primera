using CleanArchitecture.Aplication.Abstractions.Messaging;

namespace CleanArchitecture.Aplication.Alquileres.ReservarAlquiler;

public record ReservarAlquilerCommand(
    Guid VehiculoId,
    Guid UserId,
    DateOnly FechaInicio,
    DateOnly FechaFin

) : ICommand<Guid>;      //devuelve Id que se a creado

using CleanArchitecture.Aplication.Abstractions.Messaging;

namespace CleanArchitecture.Aplication.Vehiculos.SearchVehiculos;

public sealed record SearchVehiculosQuery(
    DateOnly fechaInicio,
    DateOnly fechaFin
) : IQuery<IReadOnlyList<VehiculoResponse>>;
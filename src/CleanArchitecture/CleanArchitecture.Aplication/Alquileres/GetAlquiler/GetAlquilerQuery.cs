using CleanArchitecture.Aplication.Abstractions.Messaging;

namespace CleanArchitecture.Aplication.Alquileres.GetAlquiler;

public sealed record GetAlquilerQuery(Guid AlquilerId) : IQuery<AlquilerResponse>;

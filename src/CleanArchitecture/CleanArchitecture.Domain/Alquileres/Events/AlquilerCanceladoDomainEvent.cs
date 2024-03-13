﻿using CleanArchitecture.Domain.Abstractions;

namespace CleanArchitecture.Domain.Alquileres.Events;

public sealed record AlquilerCanceladoDomainEvent(Guid AlquilerId) : IDomainEvent;

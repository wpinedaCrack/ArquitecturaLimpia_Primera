using CleanArchitecture.Aplication.Abstractions.Clock;
using CleanArchitecture.Aplication.Abstractions.Messaging;
using CleanArchitecture.Aplication.Exceptions;
using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Alquileres;
using CleanArchitecture.Domain.Users;
using CleanArchitecture.Domain.Vehiculos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Aplication.Alquileres.ReservarAlquiler;


internal sealed class ReservarAlquilerCommandHandler :
    ICommandHandler<ReservarAlquilerCommand, Guid>
{
    private readonly IUserRepository _userRepository;
    private readonly IVehiculoRepository _vehiculoRepository;
    private readonly IAlquilerRepository _alquilerRepository;
    private readonly PrecioService _precioService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDateTimeProvider _dateTimeProvider;

    public ReservarAlquilerCommandHandler(
        IUserRepository userRepository,
        IVehiculoRepository vehiculoRepository,
        IAlquilerRepository alquilerRepository,
        PrecioService precioService,
        IUnitOfWork unitOfWork,
        IDateTimeProvider dateTimeProvider
        )
    {
        _userRepository = userRepository;
        _vehiculoRepository = vehiculoRepository;
        _alquilerRepository = alquilerRepository;
        _precioService = precioService;
        _unitOfWork = unitOfWork;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<Result<Guid>> Handle(
        ReservarAlquilerCommand request,
        CancellationToken cancellationToken
        )
    {

        var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);

        if (user is null)
        {
            return Result.Failure<Guid>(UserErrors.NotFound);
        }

        var vehiculo = await _vehiculoRepository.GetByIdAsync(request.VehiculoId, cancellationToken);
        if (vehiculo is null)
        {
            return Result.Failure<Guid>(VehiculoErrors.NotFound);
        }

        var duracion = DateRange.Create(request.FechaInicio, request.FechaFin);

        //Evita que un carro se alquile cuando ya fue tomado por otra persona
        if (await _alquilerRepository.IsOverlappingAsync(vehiculo, duracion, cancellationToken))
        {
            return Result.Failure<Guid>(AlquilerErrors.Overlap);
        }

        try
        {
            var alquiler = Alquiler.Reservar(
            vehiculo,
            user.Id,
            duracion,
            _dateTimeProvider.currentTime,
            _precioService
        );

        _alquilerRepository.Add(alquiler);

        // Toma lo que esta en la memoria del entity Framework que esta pendiente de guardar y confirmar los cambios
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return alquiler.Id;

        }
        catch (ConcurrencyException)
        {
            return Result.Failure<Guid>(AlquilerErrors.Overlap);
        }
    }
}

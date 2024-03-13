﻿using CleanArchitecture.Domain.Shared;
using CleanArchitecture.Domain.Vehiculos;

namespace CleanArchitecture.Domain.Alquileres;

public class PrecioService
{
    public PrecioDetalle CalcularPrecio(Vehiculo vehiculo, DateRange periodo)
    {
        var tipoMoneda = vehiculo.Precio!.TipoMoneda;

        var precioPorPeriodo = new Moneda(//2 parametros . Monto de Dinero y Tipo de Moneda
            periodo.CantidadDias * vehiculo.Precio.Monto,
            tipoMoneda);

        decimal porcentageChange = 0;
        //Agregar accesorios y evalua cada Accesorio
        foreach (var accesorio in vehiculo.Accesorios)
        {
            porcentageChange += accesorio switch // switch Modeno
            {
                Accesorio.AppleCar or Accesorio.AndroidCar => 0.05m,
                Accesorio.AireAcondicionado => 0.01m,
                Accesorio.Mapas => 0.01m,
                _ => 0  // es como si fuera ELSE
            };
        }

        var accesorioCharges = Moneda.Zero(tipoMoneda);

        if (porcentageChange > 0)
        {
            accesorioCharges = new Moneda(
                precioPorPeriodo.Monto * porcentageChange,
                tipoMoneda
            );
        }

        var precioTotal = Moneda.Zero();
        precioTotal += precioPorPeriodo;

        if (!vehiculo!.Mantenimiento!.IsZero())
        {
            precioTotal += vehiculo.Mantenimiento;
        }

        precioTotal += accesorioCharges;


        return new PrecioDetalle(
            precioPorPeriodo,
            vehiculo.Mantenimiento,
            accesorioCharges,
            precioTotal
            );
    }
}
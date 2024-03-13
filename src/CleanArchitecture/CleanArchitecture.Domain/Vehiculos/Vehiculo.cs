using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Shared;

namespace CleanArchitecture.Domain.Vehiculos;

public sealed class Vehiculo : Entity
{
    private Vehiculo() { }

    public Vehiculo(
       Guid id,
       Modelo modelo,
       Vin vin,
       Moneda precio,
       Moneda mantenimiento,
       DateTime? fechaUltimaAlquiler,
       List<Accesorio> accesorios,
       Direccion? direccion
       ) : base(id)
    {
        Modelo = modelo;
        Vin = vin;
        Precio = precio;
        Mantenimiento = mantenimiento;
        FechaUltimaAlquiler = fechaUltimaAlquiler;
        Accesorios = accesorios;
        Direccion = direccion;
    }

    public Modelo? Modelo { get; private set; }
    public Vin? Vin { get; private set; }
    #region representaba entidad Direccion
    /*public string? Calle { get; private set; }
    public string? Departamento { get; private set; }
    public string? Provincia { get; private set; }
    public string? Ciudad { get; private set; }
    public string? Pais { get; private set; }*/
    #endregion
    public Direccion? Direccion { get; private set; }
    #region Representaba valores de Moneda
    /* decimal? Precio { get; private set; }
    public string? TipoMoneda { get; private set; }
    public string? Mantenimiento { get; private set; }
    public string? MantenimientoTipoMoneda { get; private set; }
    public DateTime? FechaUltimoAlquiler { get; private set; }*/
    #endregion
    public Moneda? Precio { get; private set; }
    public Moneda? Mantenimiento { get; private set; }
    public DateTime? FechaUltimaAlquiler { get; internal set; }
    public List<Accesorio> Accesorios { get; private set; } = new();
    
}
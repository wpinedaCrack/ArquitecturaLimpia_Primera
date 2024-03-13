using CleanArchitecture.Aplication.Abstractions.Clock;
using CleanArchitecture.Aplication.Abstractions.Data;
using CleanArchitecture.Aplication.Abstractions.Email;
using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Alquileres;
using CleanArchitecture.Domain.Users;
using CleanArchitecture.Domain.Vehiculos;
using CleanArchitecture.Infrastructure.Clock;
using CleanArchitecture.Infrastructure.Data;
using CleanArchitecture.Infrastructure.Email;
using CleanArchitecture.Infrastructure.Repositories;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {

        services.AddTransient<IDateTimeProvider, DateTimeProvider>();
        services.AddTransient<IEmailService, EmailService>();

        var connectionString = configuration.GetConnectionString("Database")
             ?? throw new ArgumentNullException(nameof(configuration));  // Sino existe Database ERROR

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            // Conectarse a postgreSQL y aplica Nomenclatura con metodo UseSnakeCaseNamingConvention
            options.UseNpgsql(connectionString).UseSnakeCaseNamingConvention(); 
        });

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IVehiculoRepository, VehiculoRepository>();
        services.AddScoped<IAlquilerRepository, AlquilerRepository>();

        // IUnitOfWork envia los datos del entity framework hacia la base de datos.
        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());

        services.AddSingleton<ISqlConnectionFactory>(_ => new SqlConnectionFactory(connectionString));

        SqlMapper.AddTypeHandler(new DateOnlyTypeHandler());
        return services;
    }

}
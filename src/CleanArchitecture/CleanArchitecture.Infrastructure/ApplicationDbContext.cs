using CleanArchitecture.Aplication.Exceptions;
using CleanArchitecture.Domain.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace CleanArchitecture.Infrastructure;

public sealed class ApplicationDbContext : DbContext, IUnitOfWork
{
    private readonly IPublisher _publisher;

    public ApplicationDbContext(DbContextOptions options, IPublisher publisher) : base(options)
    {
        _publisher = publisher;
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
    public override async Task<int> SaveChangesAsync(
       CancellationToken cancellationToken = default
       )
    {
        try
        {
            //Propio de EntityFramework y lleva lo que esta en memoria a la base de datos
            var result = await base.SaveChangesAsync(cancellationToken);

            await PublishDomainEventsAsync();

            return result;
        }
        catch (DbUpdateConcurrencyException ex)
        {
            throw new ConcurrencyException("La excepcion por concurrencia se disparo", ex);
        }
    }

    //LLama todos los entities domain Events
    private async Task PublishDomainEventsAsync()
    {
        var domainEvents = ChangeTracker
            .Entries<Entity>()
            .Select(entry => entry.Entity)
            .SelectMany(entity =>
            {
                var domainEvents = entity.GetDomainEvents();
                entity.ClearDomainEvents();
                return domainEvents;
            }).ToList();

        //Publicar todos los domain events
        foreach (var domainEvent in domainEvents)
        {
            await _publisher.Publish(domainEvent);
        }

    }
}
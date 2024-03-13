//using CleanArchitecture.Api.Extensions;
using CleanArchitecture.Api.Extensions;
using CleanArchitecture.Aplication;
using CleanArchitecture.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(); //nuevo

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.ApplyMigration();

app.SeedData();// LLama la clase que genera la data de pruebas DAPPER

app.UseCustomExceptionHandler();

app.MapControllers(); //Nuevo

app.Run();
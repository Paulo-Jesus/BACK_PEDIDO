using API.Common;
using BusinessLayer.Services;
using BusinessLayer.Services.Interfaces;
using DataLayer.Database;
using DataLayer.Repositories;
using DataLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var Cors = APIVariables.Cors;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Cors
builder.Services.AddCors(options => options.AddPolicy(APIVariables.AllowWebapp,
                                    builder => builder
                                                    .AllowAnyOrigin()
                                                    .AllowAnyHeader()
                                                    .AllowAnyMethod()
                                                    ));

builder.Services.AddScoped<IEstadoRepository, EstadoRepository>();
builder.Services.AddScoped<IEstadoService, EstadoService>();

builder.Services.AddScoped<IEmpresaRepository, EmpresaRepository>();
builder.Services.AddScoped<IEmpresaService, EmpresaService>();

builder.Services.AddScoped<ISeguridadRepository, SeguridadRepository>();
builder.Services.AddScoped<ISeguridadService, SeguridadService>();


//Add Context
builder.Services.AddDbContext<PedidosDatabaseContext>
    (
        options => options.UseSqlServer(builder.Configuration.GetConnectionString(APIVariables.ConnectionString))
    );

var app = builder.Build();

app.UseCors(Cors);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

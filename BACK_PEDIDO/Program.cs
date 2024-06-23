using BACK_PEDIDO.Models;
using BusinesssLayer;
using BusinesssLayer.Interfaces;
using DataLayer.COMMON;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<RestauranteService>();

builder.Services.AddScoped<IUsuario, UsuarioService>();
builder.Services.AddScoped<IRestaurantes, RestauranteService>();

builder.Services.AddDbContext<BdPedidosContext> (
    opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("BD_PEDIDO"))
);

/*CORS PRA CONECTAR ANGULAR*/
builder.Services.AddCors(opt =>
    opt.AddPolicy(Common.corsName, builder =>
        {
            builder.AllowAnyOrigin().
            AllowAnyHeader().AllowAnyMethod();
        })
    );
    

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors(Common.corsName);

app.MapControllers();

app.Run();

using BACK_PEDIDO.Models;
using BusinesssLayer.Services.Estados_Act_Inac;
using BusinesssLayer.Services.Roles;
using EntityLayer.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Reflection;
using System.Security.AccessControl;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


//Add Servicio de roles
builder.Services.AddScoped<RolService>();
//Add Servicio de Estados
builder.Services.AddScoped<EstadoService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<BdPedidosContext> (
    opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("BD_PEDIDO"))
);

//CORS PARA ANGULAR 
//CORS
builder.Services.AddCors(opt =>
opt.AddPolicy("Cors", builder =>
{
    builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
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
app.UseCors("Cors");

app.MapControllers();

app.Run();

using API.Common;
using BusinessLayer.Services.Pedidos.Pedidos;
using BusinessLayer.Services.Seguridad.Usuarios;
using BusinesssLayer.Services.Estados_Act_Inac;
using BusinesssLayer.Services.Roles;
using DataLayer.Database;
using DataLayer.Repositories.Seguridad.Usuarios;
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

builder.Services.AddScoped<IUsuariosRepository, UsuariosRepository>();
builder.Services.AddScoped<IUsuariosService, UsuariosService>();


//Add Servicio de roles
builder.Services.AddScoped<RolService>();
//Add Servicio de Estados
builder.Services.AddScoped<EstadoService>();
//Add Servicio de Pedidos
builder.Services.AddScoped<PedidosService>();

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
app.UseCors(APIVariables.AllowWebapp);

app.MapControllers();

app.Run();

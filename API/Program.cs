using API.Common;
using BusinessLayer.Services.Login;
using BusinessLayer.Services.Parametros;
using BusinessLayer.Services.Pedidos.HistorialPedidos;
using BusinessLayer.Services.Seguridad.CrearPerfil;
using BusinessLayer.Services.Seguridad.Usuarios;
using DataLayer.Database;
using DataLayer.Pedidos.HistorialPedidos;
using DataLayer.Repositories.Login;
using DataLayer.Repositories.Parametros;
using DataLayer.Repositories.Seguridad.CrearPerfil;
using DataLayer.Repositories.Seguridad.Usuarios;
using DataLayer.Utilities;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

var key = Encoding.ASCII.GetBytes("@2024_cl@ve_de@ccesoApl1cac1on@2023_cl@_154920$#@_++&&//_2023$$0o_&%###9000");
var jwtSettings = builder.Configuration.GetSection("Jwt");

// Cors
builder.Services.AddCors(options => options.AddPolicy("AllowWebapp",
                                    builder => builder
                                                    .AllowAnyOrigin()
                                                    .AllowAnyHeader()
                                                    .AllowAnyMethod()));


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

builder.Services.AddScoped<ILoginRepository, LoginRepository>();
builder.Services.AddScoped<ILoginService, LoginService>();

builder.Services.AddScoped<IParametrosRepository, ParametrosRepository>();
builder.Services.AddScoped<IParametrosService, ParametrosService>();

builder.Services.AddScoped<IHistorialPedidosRepository, HistorialPedidosRepository>();
builder.Services.AddScoped<IHistorialPedidosService, HistorialPedidosService>();

builder.Services.AddScoped<ICrearPerfilRepository, CrearPerfilRepository>();
builder.Services.AddScoped<ICrearPerfilService, CrearPerfilService>();

builder.Services.AddScoped<IUsuariosRepository, UsuariosRepository>();
builder.Services.AddScoped<IUsuariosService, UsuariosService>();

builder.Services.AddSingleton<Utility>();

//Add Context
builder.Services.AddDbContext<PedidosDatabaseContext>
    (
        options => options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString"))
    );

var app = builder.Build();




// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowWebapp");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

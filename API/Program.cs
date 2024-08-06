using API.Common;
using BusinessLayer.Services.Login;
using BusinessLayer.Services.Parametros;
using BusinessLayer.Services.Pedidos.HistorialPedidos;
using BusinessLayer.Services.Pedidos.Menu;
using BusinessLayer.Services.Pedidos.Productos;
using BusinessLayer.Services.Proveedor;
using BusinessLayer.Services.Seguridad.CrearPerfil;
using BusinessLayer.Services.Seguridad.Usuarios;
using BusinessLayer.Services.Seguridad.DesbloquearCuenta;
using BusinessLayer.Services.Seguridad.Login;
using BusinessLayer.Services.Seguridad.Rol;

using DataLayer.Database;
using DataLayer.Pedidos.HistorialPedidos;
using DataLayer.Repositories.Login;
using DataLayer.Repositories.Parametros;
using DataLayer.Repositories.Pedidos.Menu;
using DataLayer.Repositories.Pedidos.Productos;
using DataLayer.Repositories.Seguridad.CrearPerfil;
using DataLayer.Repositories.Seguridad.Usuarios;
using DataLayer.Repositories.RolRepository;
using DataLayer.Utilities;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Text.Json.Serialization;
using DataLayer.Repositories.Seguridad.DesbloquearCuenta;
using DataLayer.Repositories.Proveedor;

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

//Login
builder.Services.AddScoped<ILoginRepository, LoginRepository>();
builder.Services.AddScoped<ILoginService, LoginService>();

//Parametros
builder.Services.AddScoped<IParametrosRepository, ParametrosRepository>();
builder.Services.AddScoped<IParametrosService, ParametrosService>();

//Pedidos 
builder.Services.AddScoped<IHistorialPedidosRepository, HistorialPedidosRepository>();
builder.Services.AddScoped<IHistorialPedidosService, HistorialPedidosService>();

builder.Services.AddScoped<IMenuRepository, MenuRepository>();
builder.Services.AddScoped<IMenuService, MenuService>();

builder.Services.AddScoped<IProductoRepository, ProductoRepository>();
builder.Services.AddScoped<IProductoService, ProductoService>();

//Proveedor
builder.Services.AddScoped<IProveedorService, ProveedorService>();
builder.Services.AddScoped<IProveedorRepository, ProveedorRepository>();

//Seguridad
//Crear Perfil
builder.Services.AddScoped<ICrearPerfilRepository, CrearPerfilRepository>();
builder.Services.AddScoped<ICrearPerfilService, CrearPerfilService>();

//Desbloquear Cuenta
builder.Services.AddScoped<IUsuarioDcService, UsuarioDcService>();
builder.Services.AddScoped<IUsuarioDcRepository, UsuarioDcRepository>();

//Login
builder.Services.AddScoped<ILoginServicelg, LoginServicelg>();

//Parametros
builder.Services.AddScoped<IParametrosService, ParametrosService>();

//Usuario
builder.Services.AddScoped<IUsuariosRepository, UsuariosRepository>();
builder.Services.AddScoped<IUsuariosService, UsuariosService>();

//Roll
//builder.Services.AddScoped<IRolRepository, RolRepository>();
//builder.Services.AddScoped<IRolService, RolService>();



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

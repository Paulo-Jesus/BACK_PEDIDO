using API.Common;
using BusinessLayer.Services.Pedidos.Menu;
using BusinessLayer.Services.Pedidos.Productos;
using BusinessLayer.Services.Seguridad.Parametros;
using BusinessLayer.Services.Seguridad.Usuarios;
using DataLayer.Database;
using DataLayer.Repositories.Parametros;
using DataLayer.Repositories.Pedidos.Menu;
using DataLayer.Repositories.Pedidos.Productos;
using DataLayer.Repositories.Seguridad.Usuarios;
using DataLayer.Utilities;
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

builder.Services.AddScoped<IParametrosRepository, ParametrosRepository>();
builder.Services.AddScoped<IParametrosService, ParametrosService>();

builder.Services.AddScoped<IProductoRepository, ProductoRepository>();
builder.Services.AddScoped<IProductoService, ProductoService>();

builder.Services.AddScoped<IMenuRepository, MenuRepository>();
builder.Services.AddScoped<IMenuService, MenuService>();

builder.Services.AddSingleton<Utility>();

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

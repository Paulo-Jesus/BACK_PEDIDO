using DataLayer.Database;
using Utility = DataLayer.Utilities.Utility;
using EntityLayer.Models.DTO;
using EntityLayer.Models.Mappers;
using EntityLayer.Responses;
using Microsoft.Data.SqlClient;
using EntityLayer.Models.Entities;
using DataLayer.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using Microsoft.Extensions.Configuration;


namespace DataLayer.Repositories.Seguridad.DesbloquearCuenta
{
    public class UsuarioDcRepository : IUsuarioDcRepository
    {
        private readonly PedidosDatabaseContext _context;
        private readonly string _connectionString;
        private readonly ProductoMapper productoMapper = new();
        private readonly Response response = new PedidosDatabase().DatabaseConnection;
        private SqlConnection connection = new();
        private readonly List<UsuarioBlockDTO> lista = new();
        private readonly Utility _utility;
        SqlDataReader reader = null;

        public UsuarioDcRepository(PedidosDatabaseContext context, Utility utility, IConfiguration configuration)
        {
            _connectionString = Environment.GetEnvironmentVariable(DLVariables.ConnectionString)!;
            _context = context;
            _utility = utility;
        }

        public async Task<Response> obtenerTodosUsuarios()
        {
            connection = (SqlConnection)response.Data!;

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    SqlCommand command = new SqlCommand(DLStoredProcedures.SP_Obtener_Todos_Usuario_Bloqueados, connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    await connection.OpenAsync();

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            UsuarioBlockDTO usuario = new UsuarioBlockDTO
                            {
                                Nombre = reader.GetString(0),
                                Correo = reader.GetString(1),
                                NombreRol = reader.GetString(2),
                                NombreEstado = reader.GetString(3),
                            };
                            lista.Add(usuario);
                        }
                        response.Code = ResponseType.Success;
                        response.Message = DLMessages.ListadoUsuarios;
                        response.Data = lista;
                        await connection.CloseAsync();
                    }
                }
            }
            catch (Exception ex) 
            {
                response.Code = ResponseType.Error;
                response.Message = ex.Message;
                response.Data = ex.Data;
                await connection.CloseAsync();
            }
            return response;
        }

        public async Task<Response> buscarUsuarioBloqueado(string correo)
        {
            connection = (SqlConnection)response.Data!;
            UsuarioBlockDTO user = new UsuarioBlockDTO();
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    if (validarCampoVacio(correo))
                    {
                        SqlCommand command = new SqlCommand(DataLayer.Common.DLStoredProcedures.SP_Buscar_Por_Correo, connection);
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue(DLVariables.CorreoU, correo);
                        await connection.OpenAsync();

                        await command.ExecuteNonQueryAsync();

                        SqlDataReader reader = await command.ExecuteReaderAsync();

                        if (await reader.ReadAsync())
                        {
                            user = new UsuarioBlockDTO
                            {
                                Nombre = reader.GetString(0),
                                Correo = reader.GetString(1),
                                NombreRol = reader.GetString(2),
                                NombreEstado = reader.GetString(3),
                            };
                            response.Code = ResponseType.Success;
                            response.Message = DLMessages.ListadoUsuarios;
                            response.Data = user;

                            return response;
                        }
                        else
                        {
                            response.Code = ResponseType.Error;
                            response.Message = DLMessages.ListaVacia;
                            response.Data = String.Empty;
                        }

                        await connection.CloseAsync();

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return response;
        }

        public async Task<Response> DesbloquearUsuario(string correo)
        {
            connection = (SqlConnection)response.Data!;
            if (validarCampoVacio(correo))
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(_connectionString))
                    {
                        SqlCommand command = new SqlCommand(DataLayer.Common.DLStoredProcedures.SP_Desbloquear_Usuario, connection);
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue(DLVariables.CorreoU, correo);

                        await connection.OpenAsync();

                        await command.ExecuteNonQueryAsync();

                        response.Code = ResponseType.Success;
                        response.Message = DLMessages.Msj_Usuario_Unblock;
                        response.Data = String.Empty;

                        await connection.CloseAsync();

                    }
                }
                catch (Exception ex)
                {
                    response.Code = ResponseType.Error;
                    response.Message = DLMessages.Msj_Usuario_block;
                }
            }
            return response;
        }


        public bool validarCampoVacio(string texto)
        {
            bool validar = false;
            if (!string.IsNullOrWhiteSpace(texto))
            {
                validar = true;
            }
            return validar;
        }

    }
}

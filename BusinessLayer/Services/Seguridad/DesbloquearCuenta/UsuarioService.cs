using DataLayer.Common;
using EntityLayer.Models.DTO;
using EntityLayer.Responses;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services.Seguridad.DesbloquearCuenta
{
    public class UsuarioService : IUsuario
    {
        private readonly string _connectionString;
        private readonly IConfiguration _configuration;
        private readonly List<UsuarioBlockDTO> lista = new ();
        private readonly Response response = new Response ();

        public UsuarioService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString(DataLayer.Common.DLVariables.ConnectionString)!;
            _configuration = configuration;
        }

        public async Task<Response> obtenerTodosUsuarios()
        {
            using (SqlConnection conn = new(_connectionString))
            {
                SqlCommand command = new SqlCommand(DataLayer.Common.DLStoredProcedures.SP_Obtener_Todos_Usuario_Bloqueados, conn);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                await conn.OpenAsync();

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

                }
                await conn.CloseAsync();
            }
            return response;
        }

        public async Task<Response> buscarUsuarioBloqueado(string correo)
        {
            UsuarioBlockDTO user = new UsuarioBlockDTO();
            try
            {
                if (validarCampoVacio(correo))
                {
                    using (SqlConnection conn = new(_connectionString))
                    {
                        SqlCommand command = new SqlCommand(DataLayer.Common.DLStoredProcedures.SP_Buscar_Por_Correo, conn);
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue(BusinessLayer.Common.BLRows.CorreoU, correo);
                        await conn.OpenAsync();

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
                        else {
                            response.Code = ResponseType.Error;
                            response.Message = DLMessages.ListaVacia;
                            response.Data = String.Empty;
                        }

                        await conn.CloseAsync();
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
            if (validarCampoVacio(correo))
            {
                try
                {
                    using (SqlConnection conn = new(_connectionString))
                    {
                        SqlCommand command = new SqlCommand(DataLayer.Common.DLStoredProcedures.SP_Desbloquear_Usuario, conn);
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue(BusinessLayer.Common.BLRows.CorreoU, correo);

                        await conn.OpenAsync();

                        await command.ExecuteNonQueryAsync();

                        response.Code = ResponseType.Success;
                        response.Message = DLMessages.Msj_Usuario_Unblock;
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

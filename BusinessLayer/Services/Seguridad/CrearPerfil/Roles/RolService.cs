
using BusinessLayer.Common;
using DataLayer.Database;
using EntityLayer.Model;
using EntityLayer.Responses;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data;


namespace BusinesssLayer.Services.Roles
{
    public class RolService
    {

        private readonly PedidosDatabaseContext _context;
        Response response= new Response();
        private readonly string? cadenaConexion;
        BLRows mensajes = new BLRows();
        Comandos param;

        public RolService(PedidosDatabaseContext context, IConfiguration config)
        {
            _context = context;
            param = (Comandos)mensajes.parametros();

            cadenaConexion = config.GetConnectionString("BD_PEDIDO");

        }

        public async Task<Response> GetListRoles()
        {
            try
            {
                
                List<RolesDTO> lisRol = await _context.Rols.Select(r=> new RolesDTO(r.Nombre, r.IdEstado)).ToListAsync();

                response.Data = lisRol;
                response.Message = param.Message;
                response.Code=ResponseType.Success;
               
            }
            catch (Exception ex)
            {
                response.Code = ResponseType.Error; 
                response.Message=param.MensajeError + ex.Message;
            }
            return response;
        }




        public Response AddRol(RolesDTO rol)
        {
            Comandos con = (Comandos)mensajes.parametrosAddRol();
            using (SqlConnection connection = new SqlConnection(cadenaConexion))
            {
                try
                {
                   
                    connection.Open();

                    
                    SqlCommand existsCommand = new SqlCommand(con.QueryConsulta, connection);
                    existsCommand.Parameters.AddWithValue(param.NombreRolquery, rol.Nombre);
                    int count = (int)existsCommand.ExecuteScalar();

                    if (count > 0)
                    {
                        response.Data = null;
                        response.Code = ResponseType.Error;
                        response.Message = con.MensajeError;
                        return response;
                    }

                    SqlCommand command = new SqlCommand(con.QueryStoredProcedure, connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue(con.NombreRolquery, rol.Nombre);
                    command.Parameters.AddWithValue(con.IdEstadoQuery, rol.Estado);
                    command.ExecuteNonQuery();

                    response.Data = rol;
                    response.Message= con.Message;
                    response.Code = ResponseType.Success;
                }
                catch (Exception ex)
                {
                    response.Code = ResponseType.Error;
                    response.Message = con.MensajeError2 + ex.Message;
                }
                finally
                {
                    connection.Close();
                }

                return response;
            }
        }

        public Response updateRol(RolesDTO rol)
        {
            Comandos con = (Comandos)mensajes.parametrosUpdateRolQuery();
            using (SqlConnection connection = new SqlConnection(cadenaConexion))
            {
                try
                {
                   
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(con.QueryStoredProcedure, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue(param.NombreRolquery, rol.Nombre);
                        command.Parameters.AddWithValue(param.IdEstadoQuery, rol.Estado);
                        command.ExecuteNonQuery();
                        connection.Close();

                        response.Data = rol;
                        response.Code = ResponseType.Success;
                    }
                }
                catch (Exception ex)
                {
                    response.Code = ResponseType.Error;
                    response.Message = ex.Message;
                }
            }

            return response;
        }

    }
}

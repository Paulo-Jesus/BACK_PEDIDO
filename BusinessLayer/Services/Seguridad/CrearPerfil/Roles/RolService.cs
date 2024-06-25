
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

        public RolService(PedidosDatabaseContext context, IConfiguration config)
        {
            _context = context;
            cadenaConexion= config.GetConnectionString("BD_PEDIDO");

        }

        public async Task<Response> GetListRoles()
        {
            try
            {
                List<RolesDTO> lisRol = await _context.Rols.Select(r=> new RolesDTO(r.Nombre, r.IdEstado)).ToListAsync();

                response.Data = lisRol;
                response.Message = "Lista Generada con éxito";
                response.Code=ResponseType.Success;
               
            }
            catch (Exception ex)
            {
                response.Code = ResponseType.Error; 
                response.Message=ex.Message;
            }
            return response;
        }




        public Response AddRol(RolesDTO rol)
        {
            using (SqlConnection connection = new SqlConnection(cadenaConexion))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("usp_crearRol", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Nombre", rol.Nombre);
                    command.Parameters.AddWithValue("@IdEstado", rol.Estado);
                    command.ExecuteNonQuery();
                    connection.Close();

                    response.Data = rol;
                    response.Code = ResponseType.Success;
                }
                catch (Exception ex)
                {
                    response.Data = null;
                    response.Message = ex.Message;
                    response.Code = ResponseType.Error;
                }
            }
            return response;
        }

        public Response updateRol(RolesDTO rol)
        {
            using (SqlConnection connection = new SqlConnection(cadenaConexion))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("sp_updateRol", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@NombreRol", rol.Nombre);
                        command.Parameters.AddWithValue("@IdEstado", rol.Estado);
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

using BACK_PEDIDO.Models;
using EntityLayer.Model;
using EntityLayer.Response;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BusinesssLayer.Services.Roles
{
    public class RolService
    {

        private readonly BdPedidosContext _context;
        Response response= new Response();
        private readonly string? cadenaConexion;

        public RolService(BdPedidosContext context, IConfiguration config)
        {
            _context = context;
            cadenaConexion= config.GetConnectionString("BD_PEDIDO");

        }



        public async Task<Response> GetListRoles()
        {
            try
            {
                List<RolesDTO> lisRol = await _context.Rols.Select(r => new RolesDTO(r.Nombre)).ToListAsync();

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




        public Response AddRol(Rol rol)
        {
            using (SqlConnection connection = new SqlConnection(cadenaConexion))
            {
                try
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand("usp_crearRol", connection);
                    command.Parameters.AddWithValue("@Id", rol.IdRol);
                    command.Parameters.AddWithValue("@Nombre", rol.Nombre);
                    command.Parameters.AddWithValue("@Estado", rol.Nombre);
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

    }
}

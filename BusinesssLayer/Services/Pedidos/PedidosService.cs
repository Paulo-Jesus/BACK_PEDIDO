using BACK_PEDIDO.Models;
using EntityLayer.Model;
using EntityLayer.Response;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinesssLayer.Services.Pedidos
{
    public class PedidosService
    {

        private readonly BdPedidosContext _context;
        Response response = new Response();
        private readonly string? cadenaConexion;

        public PedidosService(BdPedidosContext context, IConfiguration conf)
        {
            _context = context;
            cadenaConexion = conf.GetConnectionString("BD_PEDIDO");

        }


        public async Task<Response> GetListPedidos()
        {
            using (SqlConnection connection = new SqlConnection(cadenaConexion))
            {
                try
                {
                   await connection.OpenAsync();

                    SqlCommand command = new SqlCommand("sp_consultarPedidos", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        List<PedidosDto> pedidos = new List<PedidosDto>();
                        while (await reader.ReadAsync())
                        {
                            pedidos.Add(new PedidosDto
                            {
                                FechaPedido = reader.GetDateTime(0).ToString("d"),
                                NombreUsuario = reader.GetString(1),
                                NombrePedido = reader.GetString(2),
                                PrecioProducto = reader.GetDecimal(3),
                                Cantidad = 1 
                            });
                        }
                        response.Data = pedidos;
                        response.Code = ResponseType.Success; 
                    }


                }
                catch (Exception ex)
                {
                    response.Code = ResponseType.Error;
                    response.Message = ex.Message;
                }
                return response;
            }
        }


    }
}

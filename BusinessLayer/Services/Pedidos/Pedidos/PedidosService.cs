using BusinessLayer.Common;
using DataLayer.Database;
using EntityLayer.Model;
using EntityLayer.Responses;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services.Pedidos.Pedidos
{
    public class PedidosService
    {

        private readonly PedidosDatabaseContext _context;
        Response response = new Response();
        private readonly string? cadenaConexion;
        BLRows mensajes = new BLRows();
        Comandos param;

        public PedidosService(PedidosDatabaseContext context, IConfiguration conf)
        {
            _context = context;
            cadenaConexion = conf.GetConnectionString("BD_PEDIDO");
            param = (Comandos)mensajes.parametros();

        }


        public async Task<Response> GetListPedidos()
        {
            Comandos con = (Comandos)mensajes.parametrosPedidos();
            using (SqlConnection connection = new SqlConnection(cadenaConexion))
            {
                try
                {
                    await connection.OpenAsync();

                    SqlCommand command = new SqlCommand(con.QueryStoredProcedure, connection);
                    command.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        List<PedidosDto> pedidos = new List<PedidosDto>();
                        while (await reader.ReadAsync())
                        {
                            pedidos.Add(new PedidosDto
                            {
                                FechaPedido = reader.GetDateTime(1).ToString(con.ParametroQuery),
                                NombreUsuario = reader.GetString(2),
                                NombrePedido = reader.GetString(3),
                                PrecioProducto = reader.GetDecimal(4),
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

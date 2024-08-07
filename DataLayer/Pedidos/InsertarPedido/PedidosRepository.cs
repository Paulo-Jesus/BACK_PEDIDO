using DataLayer.Database;
using EntityLayer.Models.DTO;
using EntityLayer.Responses;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Pedidos.InsertarPedido
{
    public class PedidosRepository : IPedidosRepository
    {
        private readonly PedidosDatabaseContext _context;
        private readonly Response response = new PedidosDatabase().DatabaseConnection;
        private SqlConnection connection = new();

        public PedidosRepository(PedidosDatabaseContext context)
        {
            _context = context;
        }

        public async Task<Response> InsertarPedido(PedidosInsertarDto pedido)
        {
            connection = (SqlConnection)response.Data;
            try
            {
                SqlCommand command = new SqlCommand("SP_INS_PEDIDO", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@PI_IDUSUARIO", pedido.idUsuario);
                command.Parameters.AddWithValue("@PI_IDPROVEEDOR", pedido.idProveedor);
                command.Parameters.AddWithValue("@PI_IDPRODUCTO", pedido.idProducto);

                int result = await command.ExecuteNonQueryAsync();

                if(result != 0)
                {
                    response.Code = ResponseType.Success;
                    response.Message = "Pedido ingresado correctamente";
                    response.Data = String.Empty;
                    return response;
                }
                else
                {
                    response.Code = ResponseType.Error;
                    response.Message = "Pedido no se pudo ingresar";
                    return response;
                }

            }catch (Exception ex)
            {
                response.Code = ResponseType.Success;
                response.Message = $"Error ocurrio en el proceso ";
                response.Data = ex.Data;
                return response;
            }

            return response;
        }
    }
}

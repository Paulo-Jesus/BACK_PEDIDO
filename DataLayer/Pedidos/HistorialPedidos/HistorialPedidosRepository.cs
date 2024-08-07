using DataLayer.Database;
using EntityLayer.Responses;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;

namespace DataLayer.Pedidos.HistorialPedidos
{
    public class HistorialPedidosRepository : IHistorialPedidosRepository
    {
        private readonly Response response = new PedidosDatabase().DatabaseConnection;
        private readonly DataSet dataSet = new();
        private SqlConnection connection = new();

        public async Task<Response> GetListPedidos()
        {
            if (response.Code == ResponseType.Error)
            {
                return response;
            }

            connection = (SqlConnection)response.Data!;

            try
            {
                SqlCommand command = new("sp_consultarPedidos", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                var num = await command.ExecuteNonQueryAsync();

                if (num < 0) //-1
                {
                    SqlDataAdapter dataAdapter = new(command);
                    dataAdapter.Fill(dataSet);

                    response.Code = ResponseType.Success;
                    response.Message = "Pedidos";
                    response.Data = dataSet;
                }
                else
                {
                    response.Code = ResponseType.Error;
                    response.Message = "Lista Vacia";
                    response.Data = null;
                }

            }
            catch (Exception ex)
            {
                response.Code = ResponseType.Error;
                response.Message = ex.Message;
                response.Data = ex.Data;
            }
            finally
            {
                if (connection.State > 0)
                {
                    await connection.CloseAsync();
                }
            }
            return response;
        }
    }
}

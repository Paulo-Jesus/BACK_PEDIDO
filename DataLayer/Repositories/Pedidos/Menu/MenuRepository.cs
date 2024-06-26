using DataLayer.Database;
using Utility = DataLayer.Utilities.Utility;
using EntityLayer.Models.DTO;
using EntityLayer.Models.Mappers;
using EntityLayer.Responses;
using Microsoft.Data.SqlClient;
using DataLayer.Common;

namespace DataLayer.Repositories.Pedidos.Menu
{
    public class MenuRepository
    {
        private readonly PedidosDatabaseContext _context;
        private readonly MenuMapper menuMapper = new();
        private readonly Response response = new PedidosDatabase().DatabaseConnection;
        private SqlConnection connection = new();
        private readonly Utility _utility;
        private DLVariables query = new DLVariables();

        public MenuRepository(PedidosDatabaseContext context, Utility utility) 
        {
            _context = context;
            _utility = utility;
        }

        public async Task<Response> RegistrarMenu(MenuDTO menuDTO)
        {
            connection = (SqlConnection)response.Data!;

            try
            {
                SqlCommand command = new(query.QueryValidateMenu, connection);
                int result = (int)await command.ExecuteNonQueryAsync();

                if (result <= 0)
                {


                    response.Code = ResponseType.Success;
                    response.Message = DLMessages.MenuCreado;
                    response.Data = null;
                }
                else
                {
                    // Mandar a actualizar
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

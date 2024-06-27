using DataLayer.Database;
using Utility = DataLayer.Utilities.Utility;
using EntityLayer.Models.DTO;
using EntityLayer.Models.Mappers;
using EntityLayer.Responses;
using Microsoft.Data.SqlClient;
using DataLayer.Common;
using System.Data;
using EntitiLayer.Models.Entities;

namespace DataLayer.Repositories.Pedidos.Menu
{
    public class MenuRepository : IMenuRepository
    {
        private readonly PedidosDatabaseContext _context;
        private readonly MenuMapper menuMapper = new();
        private readonly Response response = new PedidosDatabase().DatabaseConnection;
        private Response getResp = new Response();
        private SqlConnection connection = new();
        private readonly Utility _utility;

        public MenuRepository(PedidosDatabaseContext context, Utility utility)
        {
            _context = context;
            _utility = utility;
        }

        public async Task<Response> RegistrarMenu(int IdProveedor, string HoraInicio, string HoraFin, int[] IdProductos)
        {
            connection = (SqlConnection)response.Data!;

            try
            {
                DataTable dtIntArray = new DataTable();
                dtIntArray.Columns.Add("IntValue", typeof(int));

                foreach (int idProducto in IdProductos)
                {
                    dtIntArray.Rows.Add(idProducto);
                }


                getResp = await ExisteMenu(IdProveedor);

                if (Convert.ToInt32(getResp.Data) <= 0)
                {
                    SqlCommand command = new(DLStoredProcedures.SP_GeneralValidation, connection);
                    command = new(DLStoredProcedures.SP_RegistrarMenu, connection);
                    command.Parameters.Add(new SqlParameter("@IdProveedor", SqlDbType.Int)).Value = IdProveedor;
                    command.Parameters.Add(new SqlParameter("@HoraInicio", SqlDbType.VarChar, 10)).Value = HoraInicio;
                    command.Parameters.Add(new SqlParameter("@HoraFin", SqlDbType.VarChar, 10)).Value = HoraFin;
                    command.CommandType = CommandType.StoredProcedure;

                    // Parámetro del tipo de tabla (INTARRAYTYPE)
                    SqlParameter parameter = command.Parameters.AddWithValue("@IntArray", dtIntArray);
                    parameter.SqlDbType = SqlDbType.Structured;

                    // Nombre del nuevo tipo de Dato en SQL Server
                    parameter.TypeName = "dbo.INTARRAYTYPE";

                    int newResult = await command.ExecuteNonQueryAsync();

                    if (newResult <= 0)
                    {
                        response.Code = ResponseType.Success;
                        response.Message = DLMessages.MenuCreado;
                        response.Data = null;
                    }
                    else
                    {
                        throw new Exception(DLMessages.ErrorMenuCreado);
                    }
                }
                else
                {
                    Console.WriteLine("Ya existe");
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

        public async Task<Response> ActualizarMenu(int IdProveedor, int[] IdProductos)
        {
            try
            {
                

                response.Code = ResponseType.Success;
                response.Message = DLMessages.MenuActualizado;
                response.Data = null;
            }
            catch (Exception ex)
            {
                response.Code = ResponseType.Error;
                response.Message = ex.Message;
                response.Data = ex.Data;
            }
            return response;
        }

        public async Task<Response> ExisteMenu(int IdProveedor)
        {
            connection = (SqlConnection)response.Data!;
            try
            {
                SqlCommand command = new(DLStoredProcedures.SP_GeneralValidation, connection);
                command.Parameters.Add(new SqlParameter("@Type", SqlDbType.VarChar, 40)).Value = DLVariables.SP_ParamType_EMD;
                command.Parameters.Add(new SqlParameter("@IdProveedor", SqlDbType.Int)).Value = IdProveedor;
                command.CommandType = CommandType.StoredProcedure;
                int result = Convert.ToInt32(await command.ExecuteScalarAsync());

                response.Code = ResponseType.Success;
                response.Message = DLMessages.MenuExiste;
                response.Data = result;
            }
            catch (Exception ex)
            {
                response.Code = ResponseType.Error;
                response.Message = ex.Message;
                response.Data = ex.Data;
            }

            return response;
        }
    }
}

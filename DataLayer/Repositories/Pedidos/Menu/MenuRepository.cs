using DataLayer.Database;
using Utility = DataLayer.Utilities.Utility;
using EntityLayer.Models.DTO;
using EntityLayer.Models.Mappers;
using EntityLayer.Responses;
using Microsoft.Data.SqlClient;
using DataLayer.Common;
using System.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using EntityLayer.Models.Entities;

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
        SqlDataReader reader = null;

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
                dtIntArray.Columns.Add(DLVariables.IntValue, typeof(int));

                foreach (int idProducto in IdProductos)
                {
                    dtIntArray.Rows.Add(idProducto);
                }


                getResp = await DatosMenu(IdProveedor, DLVariables.SP_ParamType_EMD);

                if (Convert.ToInt32(getResp.Data) <= 0)
                {
                    SqlCommand command = new(DLStoredProcedures.SP_RegistrarMenu, connection);
                    command.Parameters.Add(new SqlParameter(DLSPParameters.IdProveedor, SqlDbType.Int)).Value = IdProveedor;
                    command.Parameters.Add(new SqlParameter(DLSPParameters.HoraInicio, SqlDbType.VarChar, 10)).Value = HoraInicio;
                    command.Parameters.Add(new SqlParameter(DLSPParameters.HoraFin, SqlDbType.VarChar, 10)).Value = HoraFin;
                    command.CommandType = CommandType.StoredProcedure;

                    // Parámetro del tipo de tabla (INTARRAYTYPE)
                    SqlParameter parameter = command.Parameters.AddWithValue(DLSPParameters.IntArray, dtIntArray);
                    parameter.SqlDbType = SqlDbType.Structured;

                    // Nombre del nuevo tipo de Dato en SQL Server
                    parameter.TypeName = DLVariables.INTARRAYTYPE;

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
                    await ActualizarMenu(IdProveedor, HoraInicio, HoraFin, IdProductos);
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

        public async Task<Response> ActualizarMenu(int IdProveedor, string HoraInicio, string HoraFin, int[] IdProductos)
        {
            connection = (SqlConnection)response.Data!;
            try
            {
                DataTable dtIntArray = new DataTable();
                dtIntArray.Columns.Add(DLVariables.IntValue, typeof(int));

                foreach (int idProducto in IdProductos)
                {
                    dtIntArray.Rows.Add(idProducto);
                }

                getResp = await DatosMenu(IdProveedor, DLVariables.SP_ParamType_IMD);
                MenuDTO? menu = getResp.Data as MenuDTO;

                SqlCommand command = new(DLStoredProcedures.SP_ActualizarMenu, connection);
                command.Parameters.Add(new SqlParameter(DLSPParameters.IdMenu, SqlDbType.Int)).Value = menu?.IdMenu;
                command.Parameters.Add(new SqlParameter(DLSPParameters.HoraInicio, SqlDbType.VarChar, 10)).Value = HoraInicio;
                command.Parameters.Add(new SqlParameter(DLSPParameters.HoraFin, SqlDbType.VarChar, 10)).Value = HoraFin;
                command.CommandType = CommandType.StoredProcedure;

                SqlParameter parameter = command.Parameters.AddWithValue(DLSPParameters.NewValues, dtIntArray);
                parameter.SqlDbType = SqlDbType.Structured;
                parameter.TypeName = DLVariables.INTARRAYTYPE;

                int newResult = await command.ExecuteNonQueryAsync();

                if (newResult < 0)
                {
                    response.Code = ResponseType.Success;
                    response.Message = DLMessages.MenuActualizado;
                    response.Data = null;
                }
                else
                {
                    throw new Exception(DLMessages.ErrorMenuActualizar);
                }
            }
            catch (Exception ex)
            {
                response.Code = ResponseType.Error;
                response.Message = ex.Message;
                response.Data = ex.Data;
            }
            return response;
        }

        /**
         * Devuelve la informacion complenta del menu, desde info general hasta los platos seleccionados
         */
        public async Task<Response> DatosMenuCompleto(int IdProveedor) 
        {
            connection = (SqlConnection)response.Data!;
            try
            {
                SqlCommand command = new(DLStoredProcedures.SP_ObtenerProductosMenu, connection);
                command.Parameters.Add(new SqlParameter(DLSPParameters.IdProveedor, SqlDbType.Int)).Value = IdProveedor;
                command.CommandType = CommandType.StoredProcedure;
                reader = await command.ExecuteReaderAsync();

                List<MenuDetalleDTO> listaMenus = new List<MenuDetalleDTO>();

                while (await reader.ReadAsync()) 
                {
                    MenuDetalleDTO menuDetalle = new MenuDetalleDTO();
                    menuDetalle.IdMenu = reader.GetInt32(DLVariables.IdMenu);
                    menuDetalle.FechaInicio = reader.GetDateTime(DLVariables.FechaInicio);
                    menuDetalle.FechaFin = reader.GetDateTime(DLVariables.FechaFin);
                    menuDetalle.IdMenuDetalle = reader.GetInt32(DLVariables.IdMenuDetalle);
                    menuDetalle.IdProducto = reader.GetInt32(DLVariables.IdProducto);

                    listaMenus.Add(menuDetalle);
                }

                response.Code = ResponseType.Success;
                response.Message = DLMessages.MenuExiste;
                response.Data = listaMenus;
            }
            catch (Exception ex) 
            {
                response.Code = ResponseType.Error;
                response.Message = ex.Message;
                response.Data = ex.Data;
            }

            return response;
        }

        /**
         * param param name="IdProveedor" Limita las consultas mostrando solo los menu de la cuenta del proveedor/restaurante
         * param name="TipoTrx" Define el tipo de transaccion a ejecutar por el SP, 
         * Si es igual a "DLVariables.SP_ParamType_EMD" devolvera 1 si existe un menu para el restaurante y 0 si no
         * Si es igual a "DLVariables.SP_ParamType_IMD" devolvera la informacion basica del menu (No trae los platos)
         */
        public async Task<Response> DatosMenu(int IdProveedor, string TipoTrx)
        {
            connection = (SqlConnection)response.Data!;
            try
            {
                if (TipoTrx.Equals(DLVariables.SP_ParamType_EMD))
                {
                    SqlCommand command = new(DLStoredProcedures.SP_GeneralValidation, connection);
                    command.Parameters.Add(new SqlParameter(DLSPParameters.Type, SqlDbType.VarChar, 40)).Value = TipoTrx;
                    command.Parameters.Add(new SqlParameter(DLSPParameters.IdProveedor, SqlDbType.Int)).Value = IdProveedor;
                    command.CommandType = CommandType.StoredProcedure;
                    int result = Convert.ToInt32(await command.ExecuteScalarAsync());

                    response.Code = ResponseType.Success;
                    response.Message = result > 0 ? DLMessages.MenuExiste : DLMessages.SinMenu;
                    response.Data = result;
                }
                else if (TipoTrx.Equals(DLVariables.SP_ParamType_IMD))
                {
                    SqlCommand command = new(DLStoredProcedures.SP_GeneralValidation, connection);
                    command.Parameters.Add(new SqlParameter(DLSPParameters.Type, SqlDbType.VarChar, 40)).Value = TipoTrx;
                    command.Parameters.Add(new SqlParameter(DLSPParameters.IdProveedor, SqlDbType.Int)).Value = IdProveedor;
                    command.CommandType = CommandType.StoredProcedure;
                    reader = command.ExecuteReader();

                    MenuDTO menu = null;

                    if (await reader.ReadAsync())
                    {
                        menu = new MenuDTO
                        {
                            IdMenu = reader.GetInt32(DLVariables.IdMenu),
                            FechaInicio = reader.GetDateTime(DLVariables.FechaInicio),
                            FechaFin = reader.GetDateTime(DLVariables.FechaFin),
                            IdProveedor = reader.GetInt32(DLVariables.IdProveedor)
                        };
                    }

                    response.Code = ResponseType.Success;
                    response.Message = DLMessages.MenuExiste;
                    response.Data = menu;
                }
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

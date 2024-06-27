using DataLayer.Common;
using DataLayer.Database;
using EntityLayer.Models.DTO;
using EntityLayer.Models.Entities;
using EntityLayer.Responses;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace DataLayer.Repositories.Parametros
{
    public class ParametrosRepository : IParametrosRepository
    {
        private readonly PedidosDatabaseContext _context;
        private readonly Response response = new PedidosDatabase().DatabaseConnection;
        private SqlConnection connection = new();

        public ParametrosRepository(PedidosDatabaseContext context)
        {
            _context = context;
        }

        public async Task MetodoEmpresasGuardar(SqlConnection connection, EmpresaDTO empresaDTO)
        {
            SqlCommand command = new(DLStoredProcedures.SP_InsertarEmpresa, connection);
            command.Parameters.Add(new SqlParameter(DLParameters.RUC, SqlDbType.VarChar, 13)).Value = empresaDTO.Ruc;
            command.Parameters.Add(new SqlParameter(DLParameters.RazonSocial, SqlDbType.VarChar, 100)).Value = empresaDTO.RazonSocial;
            command.CommandType = CommandType.StoredProcedure;

            int num = await command.ExecuteNonQueryAsync();

            if (num >= 0)
            {
                response.Code = ResponseType.Success;
                response.Message = DLMessages.EmpresaAgregada;
                response.Data = null;
            }
            else
            {
                response.Code = ResponseType.Error;
                response.Message = DLMessages.EmpresaNoAgregada;
                response.Data = null;
            }
        }

        public async Task<Response> Guardar(EmpresaDTO empresaDTO)
        {
            try
            {
                List<Empresa> empresas = await _context.Empresas
                   .Where(e => e.Ruc == empresaDTO.Ruc)
                   .ToListAsync();

                if (empresas.Count < 1)
                {
                    if (response.Code == ResponseType.Error)
                    {
                        return response;
                    }

                    connection = (SqlConnection)response.Data!;
                    await MetodoEmpresasGuardar(connection, empresaDTO);
                    return response;

                }

                await EmpresasEditar(empresaDTO);
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

        public async Task<Response> EmpresasEditar(EmpresaDTO empresaDTO)
        {
            try
            {
                Empresa? empresa = await _context.Empresas.FirstOrDefaultAsync(u => u.IdEmpresa == empresaDTO.IdEmpresa);

                empresa!.Ruc = empresaDTO.Ruc;
                empresa.RazonSocial = empresaDTO.RazonSocial;
                empresa.IdEstado = 1;

                await _context.SaveChangesAsync();

                response.Code = ResponseType.Success;
                response.Message = DLMessages.EmpresaEditada;
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
    }
}

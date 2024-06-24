using DataLayer.Database;
using DataLayer.Repositories.Interfaces;
using EntityLayer.Models.DTO;
using EntityLayer.Models.Entities;
using EntityLayer.Models.Mappers;
using EntityLayer.Responses;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace DataLayer.Repositories
{
    public class EmpresaRepository(PedidosDatabaseContext context) : IEmpresaRepository
    {
        private readonly EmpresaMapper empresaMapper = new();
        private readonly PedidosDatabaseContext _context = context;
        private readonly Response response = new PedidosDatabase().DatabaseConnection;
        private SqlConnection connection = new();

        public async Task<Response> ObtenerEmpresasEF()
        {
            try
            {
                response.Code = ResponseType.Success;
                response.Message = "Listado de Empresas";

                List<Empresa> empresas = await _context.Empresas
                    .Where(e => e.IdEstado == 1)
                    .ToListAsync();

                List<EmpresaDTO> empresasDTOs = empresas
                    .Select(empresas => empresaMapper.EmpresaToEmpresaDTO(empresas))
                    .ToList();

                response.Data = empresasDTOs;
            }
            catch (Exception ex)
            {
                response.Code = ResponseType.Error;
                response.Message = ex.Message;
                response.Data = ex.Data;
            }
            return response;
        }

        public async Task<Response> RegistrarEmpresaADO(EmpresaDTO empresaDTO)
        {
            if (response.Code == ResponseType.Error)
            {
                return response;
            }

            connection = (SqlConnection)response.Data!;

            try
            {
                SqlCommand command = new("SP_RegistrarEmpresaADO", connection);
                command.Parameters.Add(new SqlParameter("@RUC", SqlDbType.VarChar, 13)).Value = empresaDTO.Ruc;
                command.Parameters.Add(new SqlParameter("@RazonSocial", SqlDbType.VarChar, 100)).Value = empresaDTO.RazonSocial;
                command.CommandType = CommandType.StoredProcedure;

                var num = await command.ExecuteNonQueryAsync();

                if (num < 0)
                {
                    response.Code = ResponseType.Success;
                    response.Message = "Empresa creada!";
                    response.Data = null;
                }
                else
                {
                    response.Code = ResponseType.Error;
                    response.Message = "Empresa no creada.";
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

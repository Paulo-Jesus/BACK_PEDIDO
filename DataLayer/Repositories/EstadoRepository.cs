using DataLayer.Common;
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
    public class EstadoRepository(PedidosDatabaseContext context) : IEstadoRepository
    {
        private readonly PedidosDatabaseContext _context = context;
        private readonly Response response = new PedidosDatabase().DatabaseConnection;
        private readonly EstadoMapper estadoMapper = new();
        private SqlConnection connection = new();
   
        public async Task<Response> ObtenerEstadosEF()
        {
            try
            {
                response.Code = ResponseType.Success;
                response.Message = "Listado de Estados";
                List<Estado> estados = await _context.Estados.ToListAsync();
                List<EstadoDTO> estadosDTOs = estados.Select(estados => estadoMapper.EstadoToEstadoDTO(estados)).ToList();
                response.Data = estadosDTOs;
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

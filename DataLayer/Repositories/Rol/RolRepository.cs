using DataLayer.Common;
using DataLayer.Database;
using DataLayer.Utilities;
using EntityLayer.Models.DTO;
using EntityLayer.Models.Mappers;
using EntityLayer.Responses;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using EntityLayer.Models.Entities;
using DataLayer.Repositories.RolRepository;

namespace DataLayer.Repositories.RolRepository
{
    public class RolRepository : IRolRepository
    {
        
        private readonly Response response = new PedidosDatabase().DatabaseConnection;
        private readonly RolMapper rolMapper = new();
        private readonly PedidosDatabaseContext _context;
        private readonly RolRepository _rolRepository;
        private readonly string _connectionString;
        private SqlConnection connection = new();
        SqlDataReader reader = null;
        public RolRepository(PedidosDatabaseContext context, RolRepository rolRepository)
        {
            _context = context;
           _rolRepository = rolRepository;
        }

        public async Task<Response> Lista()
        {
            try
            {
                
                List<Rol> rols = await _context.Rols.ToListAsync();

                List<RolDTO> rolDTOs = rols.Select(Rols => rolMapper.RoltoRolesDTO(Rols)).ToList();

                if (rols.Count < 1)
                {
                    response.Code = ResponseType.Success;
                    response.Message = DLMessages.NoRolsRegistrados;
                    response.Data = null;

                    return response;
                }

                response.Code = ResponseType.Success;
                response.Message = DLMessages.ListadoDeRols;
                response.Data = rolDTOs;
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

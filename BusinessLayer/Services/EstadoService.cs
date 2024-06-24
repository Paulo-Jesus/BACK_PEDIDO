using BusinessLayer.Services.Interfaces;
using DataLayer.Repositories.Interfaces;
using EntityLayer.Models.DTO;
using EntityLayer.Responses;
using System.Data;

namespace BusinessLayer.Services
{
    public class EstadoService(IEstadoRepository estadoRepository) : IEstadoService
    {
        private readonly IEstadoRepository _estadoRepository = estadoRepository;
        private Response response = new();
        DataSet dataSet = new();

        public async Task<Response> ObtenerEstadosEF()
        {
            
                try
                {
                    response = await _estadoRepository.ObtenerEstadosEF();
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

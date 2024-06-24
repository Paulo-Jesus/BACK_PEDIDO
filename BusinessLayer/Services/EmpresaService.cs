using BusinessLayer.Services.Interfaces;
using DataLayer.Repositories.Interfaces;
using EntityLayer.Models.DTO;
using EntityLayer.Responses;
using System.Data;

namespace BusinessLayer.Services
{
    public class EmpresaService(IEmpresaRepository empresaRepository) : IEmpresaService
    {
        private readonly IEmpresaRepository _empresaRepository = empresaRepository;
        private Response response = new();
        DataSet dataSet = new();

        public async Task<Response> ObtenerEmpresasEF()
        {
            {
                try
                {
                    response = await _empresaRepository.ObtenerEmpresasEF();
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

        public async Task<Response> RegistrarEmpresaADO(EmpresaDTO empresaDTO)
        {
            {
                try
                {
                    response = await _empresaRepository.RegistrarEmpresaADO(empresaDTO);
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

}

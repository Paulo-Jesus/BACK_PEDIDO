using DataLayer.Repositories.Parametros;
using EntityLayer.Models.DTO;
using EntityLayer.Responses;

namespace BusinessLayer.Services.Seguridad.Parametros
{
    public class ParametrosService : IParametrosService
    {
        private readonly IParametrosRepository _parametrosRepository;
        private Response response = new();

        public ParametrosService(IParametrosRepository parametrosRepository)
        {
            _parametrosRepository = parametrosRepository;
        }

        public async Task<Response> Guardar(EmpresaDTO empresaDTO)
        {
            response = await _parametrosRepository.Guardar(empresaDTO);
            return response;
        }
    }
}

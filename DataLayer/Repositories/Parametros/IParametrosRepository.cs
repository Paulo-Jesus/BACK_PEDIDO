using EntityLayer.Models.DTO;
using EntityLayer.Responses;

namespace DataLayer.Repositories.Parametros
{
    public interface IParametrosRepository
    {
        public Task<Response> Guardar(EmpresaDTO empresaDTO);
    }
}
